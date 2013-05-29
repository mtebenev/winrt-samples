using System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Mt.Common.UiCore.Controls
{
	/// <summary>
	/// Based on LayoutAwarePage class provided in standard WinRT project templates
	/// </summary>
	[Windows.Foundation.Metadata.WebHostHidden]
	public class LayoutAwarePage : Page
	{
		private Control _control;

		/// <summary>
		/// Initializes a new instance of the <see cref="LayoutAwarePage"/> class.
		/// </summary>
		public LayoutAwarePage()
		{
			if(Windows.ApplicationModel.DesignMode.DesignModeEnabled) 
				return;

			this.Loaded += this.StartLayoutUpdates;
			this.Unloaded += this.StopLayoutUpdates;
		}

		/// <summary>
		/// Invoked as an event handler, typically on the <see cref="FrameworkElement.Loaded"/>
		/// event of a <see cref="Control"/> within the page, to indicate that the sender should
		/// start receiving visual state management changes that correspond to application view
		/// state changes.
		/// </summary>
		/// <param name="sender">Instance of <see cref="Control"/> that supports visual state
		/// management corresponding to view states.</param>
		/// <param name="e">Event data that describes how the request was made.</param>
		/// <remarks>The current view state will immediately be used to set the corresponding
		/// visual state when layout updates are requested.  A corresponding
		/// <see cref="FrameworkElement.Unloaded"/> event handler connected to
		/// <see cref="StopLayoutUpdates"/> is strongly encouraged.  Instances of
		/// <see cref="LayoutAwarePage"/> automatically invoke these handlers in their Loaded and
		/// Unloaded events.</remarks>
		/// <seealso cref="DetermineVisualState"/>
		/// <seealso cref="InvalidateVisualState"/>
		public void StartLayoutUpdates(object sender, RoutedEventArgs e)
		{
			Control control = sender as Control;
			if(control == null) 
				return;

			if(_control != null)
				throw new InvalidOperationException("The method must not be invoked twice.");

			_control = control;

			// Start listening to view state changes when there are controls interested in updates
			Window.Current.SizeChanged += this.WindowSizeChanged;

			// Set the initial visual state of the control
			string stateName = DetermineVisualState(ApplicationView.Value);
			VisualStateManager.GoToState(control, stateName, false);
		}

		private void WindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
		{
			this.InvalidateVisualState();
		}

		/// <summary>
		/// Invoked as an event handler, typically on the <see cref="FrameworkElement.Unloaded"/>
		/// event of a <see cref="Control"/>, to indicate that the sender should start receiving
		/// visual state management changes that correspond to application view state changes.
		/// </summary>
		/// <param name="sender">Instance of <see cref="Control"/> that supports visual state
		/// management corresponding to view states.</param>
		/// <param name="e">Event data that describes how the request was made.</param>
		/// <remarks>The current view state will immediately be used to set the corresponding
		/// visual state when layout updates are requested.</remarks>
		/// <seealso cref="StartLayoutUpdates"/>
		public void StopLayoutUpdates(object sender, RoutedEventArgs e)
		{
			Control control = sender as Control;
			if(control == null || _control == null) 
				return;

			_control = null;
			Window.Current.SizeChanged -= this.WindowSizeChanged;
		}

		/// <summary>
		/// Translates <see cref="ApplicationViewState"/> values into strings for visual state
		/// management within the page.  The default implementation uses the names of enum values.
		/// Subclasses may override this method to control the mapping scheme used.
		/// </summary>
		/// <param name="viewState">View state for which a visual state is desired.</param>
		/// <returns>Visual state name used to drive the
		/// <see cref="VisualStateManager"/></returns>
		/// <seealso cref="InvalidateVisualState"/>
		protected virtual string DetermineVisualState(ApplicationViewState viewState)
		{
			return viewState.ToString();
		}

		/// <summary>
		/// Updates all controls that are listening for visual state changes with the correct
		/// visual state.
		/// </summary>
		/// <remarks>
		/// Typically used in conjunction with overriding <see cref="DetermineVisualState"/> to
		/// signal that a different value may be returned even though the view state has not
		/// changed.
		/// </remarks>
		public void InvalidateVisualState()
		{
			if(_control != null)
			{
				string visualState = DetermineVisualState(ApplicationView.Value);
				VisualStateManager.GoToState(_control, visualState, false);
			}
		}
	}
}
