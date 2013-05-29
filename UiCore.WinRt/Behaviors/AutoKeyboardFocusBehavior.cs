using Windows.UI.Core;
using Windows.UI.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Mt.Common.UiCore.Behaviors
{
	/// <summary>
	/// Automatically brings focus to attached TextBox control when user types something using keyboard
	/// </summary>
	public class AutoKeyboardFocusBehavior : Behavior<TextBox>
	{
		protected override void OnAttached()
		{
			base.OnAttached();
			Window.Current.CoreWindow.KeyDown += HandleCoreWindowKeyDown;
		}

		protected override void OnDetaching()
		{
			Window.Current.CoreWindow.KeyDown -= HandleCoreWindowKeyDown;
			base.OnDetaching();
		}

		private void HandleCoreWindowKeyDown(CoreWindow sender, KeyEventArgs args)
		{
			this.AssociatedObject.Focus(FocusState.Keyboard);
		}
	}
}
