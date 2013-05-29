using System;
using Mt.Common.UiCore.MvvmCore;
using Windows.UI.Interactivity;
using Windows.UI.Xaml.Controls;

namespace Mt.Common.UiCore.Behaviors
{
	/// <summary>
	/// Represents a service that provides properties and events to control the navigation of views for view models.
	/// </summary>
	public class FrameNavigatorBehavior : Behavior<Frame>
	{
		protected override void OnAttached()
		{
			base.OnAttached();

			// Enable navigation on the new frame
			Frame frame = this.AssociatedObject;
			NavigableViewModelBase newNavigableViewModel = frame.DataContext as NavigableViewModelBase;

			if(newNavigableViewModel == null)
				throw new InvalidOperationException("Set NavigableViewModel-based class ad DataContext for the associated frame.");

			NavigationServiceFrame navigationService = new NavigationServiceFrame(newNavigableViewModel, frame);
			newNavigableViewModel.EnableNavigation(navigationService);
		}
	}
}
