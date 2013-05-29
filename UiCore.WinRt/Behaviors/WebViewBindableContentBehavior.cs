using System;
using System.IO;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Mt.Common.UiCore.Behaviors
{
	/// <summary>
	/// Makes possible to bind a WebView control to string content
	/// </summary>
	public class WebViewBindableContentBehavior : Behavior<WebView>
	{
		private string _htmlDocTemplate;

		/// <summary>
		/// Content is a string to be displayed in attached WebView
		/// </summary>
		public static readonly DependencyProperty ContentProperty =
			DependencyProperty.Register("Content", typeof(String), typeof(WebViewBindableContentBehavior), 
			new PropertyMetadata(null, HandleContentPropertyChanged));

		public string Content
		{
			get { return (string)GetValue(ContentProperty); }
			set { SetValue(ContentProperty, value); }
		}

		/// <summary>
		/// If set, then the behavior reads text at the uri and uses that as wrapper for content
		/// The text at the uri must contain {0} so the behavior will substitue the text inside the template
		/// </summary>
		public Uri HtmlDocTemplateUri { get; set; }

		protected override void OnAttached()
		{
			base.OnAttached();

			if(HtmlDocTemplateUri != null)
				LoadHtmlDocTemplate();
			else
				ReloadControlContent();
		}

		/// <summary>
		/// Loads HTML doc content and reloads webview
		/// Design note: this method intentionally made reloading webview content to not wait for template loading outside
		/// </summary>
		private async void LoadHtmlDocTemplate()
		{
			// Note: more info at: http://blog.stephencleary.com/2012/07/dont-block-on-async-code.html
			StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(HtmlDocTemplateUri).AsTask().ConfigureAwait(false);
			Stream stream = await file.OpenStreamForReadAsync().ConfigureAwait(false);

			using(TextReader reader = new StreamReader(stream))
			{
				_htmlDocTemplate = reader.ReadToEnd();
			}

			AssociatedObject.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, 
				() =>
					{
						if(this.Content != null)
							ReloadControlContent();
					});
		}

		private static void HandleContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			WebViewBindableContentBehavior behavior = (WebViewBindableContentBehavior)d;
			if(behavior.AssociatedObject != null)
				behavior.ReloadControlContent();
		}

		private void ReloadControlContent()
		{
			this.AssociatedObject.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
				() =>
				{
					string content = (!String.IsNullOrEmpty(_htmlDocTemplate) ? String.Format(_htmlDocTemplate, this.Content) : this.Content) ??
					                 String.Empty;

					if(this.AssociatedObject != null) // Detaching may occure during the this async operation
						this.AssociatedObject.NavigateToString(content);
				});
		}
	}
}
