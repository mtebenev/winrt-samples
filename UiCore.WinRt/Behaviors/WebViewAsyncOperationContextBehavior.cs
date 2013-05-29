using System;
using Mt.Common.UiCore.MvvmCore;
using Windows.UI.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Mt.Common.UiCore.Behaviors
{
	/// <summary>
	/// Starts operation on context when associated web view loads document
	/// Note: also take a look at: http://nicksnettravels.builttoroam.com/post/2012/04/21/Limitations-of-the-WebView-in-Windows-8-Metro-Apps.aspx
	/// Probably there's a better solution
	/// </summary>
	public class WebViewAsyncOperationContextBehavior : Behavior<WebView>
	{
		private AsyncOperation _operation;

		/// <summary>
		/// Bound async operation context
		/// </summary>
		public static readonly DependencyProperty AsyncOperationContextProperty =
			DependencyProperty.Register("AsyncOperationContext", typeof(AsyncOperationContext), typeof(WebViewAsyncOperationContextBehavior),
			new PropertyMetadata(DependencyProperty.UnsetValue));

		public AsyncOperationContext AsyncOperationContext
		{
			get { return (AsyncOperationContext)GetValue(AsyncOperationContextProperty); }
			set { SetValue(AsyncOperationContextProperty, value); }
		}

		/// <summary>
		/// Source dependency property. Bind same as WebView's source to let behavior know when page loading starts.
		/// </summary>
		public static readonly DependencyProperty SourceProperty =
			DependencyProperty.Register("Source", typeof(Uri), typeof(WebViewAsyncOperationContextBehavior),
			new PropertyMetadata(null, HandleSourcePropertyChanged));

		public Uri Source
		{
			get { return (Uri)GetValue(SourceProperty); }
			set { SetValue(SourceProperty, value); }
		}

		private static void HandleSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			WebViewAsyncOperationContextBehavior behavior = (WebViewAsyncOperationContextBehavior)d;
			behavior.StartOperation();
		}

		private void StartOperation()
		{
			if(this.AsyncOperationContext == null)
				throw new InvalidOperationException("Set up AsyncOperationContext property first.");

			StopOperation(); // Stop currently running operation if any

			_operation = new AsyncOperation(null);
			this.AsyncOperationContext.StartOperation(_operation);

			this.AssociatedObject.LoadCompleted += HandleLoadCompleted;
			this.AssociatedObject.NavigationFailed += HandleNavigationFailed;
		}

		private void HandleNavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
		{
			StopOperation();
		}

		private void HandleLoadCompleted(object sender, NavigationEventArgs e)
		{
			StopOperation();
		}

		private void StopOperation()
		{
			if(_operation != null)
			{
				// Unsubscribe event handlers
				this.AssociatedObject.LoadCompleted -= HandleLoadCompleted;
				this.AssociatedObject.NavigationFailed -= HandleNavigationFailed;

				this.AsyncOperationContext.StopOperation(_operation);
				_operation = null;
			}
		}
	}
}
