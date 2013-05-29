using System.Windows.Input;
using Windows.UI.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Mt.Common.UiCore.Behaviors
{
	/// <summary>
	/// The attached behavior executes a bound on item click.
	/// It passes bound item as object parameter
	/// </summary>
	public class WebViewLoadedToCommandBehavior : Behavior<WebView>
	{
		/// <summary>
		/// Bound command
		/// </summary>
		public static readonly DependencyProperty CommandProperty =
			DependencyProperty.Register("Command", typeof(ICommand), typeof(WebViewLoadedToCommandBehavior),
			new PropertyMetadata(DependencyProperty.UnsetValue));

		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		protected override void OnAttached()
		{
			base.OnAttached();
			AssociatedObject.LoadCompleted += HandleLoadCompleted;
		}

		protected override void OnDetaching()
		{
			AssociatedObject.LoadCompleted -= HandleLoadCompleted;
			base.OnDetaching();
		}


		private void HandleLoadCompleted(object sender, NavigationEventArgs navigationEventArgs)
		{
			ICommand command = Command;
			if(command != null && command.CanExecute(navigationEventArgs))
			{
				command.Execute(navigationEventArgs);
			}
		}
	}
}
