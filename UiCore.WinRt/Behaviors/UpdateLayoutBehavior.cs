using Windows.UI.Core;
using Windows.UI.Interactivity;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Mt.Common.UiCore.Behaviors
{
	/// <summary>
	/// Updates framework layout according to defined visual state.
	/// Essentially does the same as LayoutAwarePage but acts as attached behavior and does not requires class inheritance.
	/// </summary>
	public class UpdateLayoutBehavior : Behavior<Control>
	{
		protected override void OnAttached()
		{
			base.OnAttached();

			if(!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
			{
				InvalidateVisualState();
				Window.Current.SizeChanged += HandleWindowSizeChanged;
			}
		}

		protected override void OnDetaching()
		{
			if(!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
				Window.Current.SizeChanged -= HandleWindowSizeChanged;

			base.OnDetaching();
		}

		private void HandleWindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
		{
			InvalidateVisualState();
		}

		private void InvalidateVisualState()
		{
			string stateName = ApplicationView.Value.ToString();
			VisualStateManager.GoToState(this.AssociatedObject, stateName, false);
		}
	}
}
