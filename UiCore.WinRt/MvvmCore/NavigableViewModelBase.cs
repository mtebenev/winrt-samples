using System;
using Windows.UI.Xaml.Navigation;

namespace Mt.Common.UiCore.MvvmCore
{
	/// <summary>
	/// Supports view navigation and state persistence.
	/// </summary>
	public abstract class NavigableViewModelBase : ViewModelBase
	{
		private INavigationService _navigationService;

		/// <summary>
		/// Occurs when the View Model navigation is enabled.
		/// </summary>
		public event EventHandler NavigationEnabled;

		/// <summary>
		/// Gets a value indicating whether this instance can navigate.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance can navigate; otherwise, <c>false</c>.
		/// </value>
		public bool CanNavigate { get { return _navigationService != null; } }

		/// <summary>
		/// Gets a value that indicates whether there is at least one entry in back navigation history.
		/// </summary>
		/// <value>
		///		<c>true</c> if there is at least one entry in back navigation history; otherwise, <c>false</c>.
		/// </value>
		public bool CanGoBack
		{
			get
			{
				return this.CanNavigate && _navigationService.CanGoBack;
			}
		}

		internal void EnableNavigation(INavigationService navigationService)
		{
			if(_navigationService == null)
			{
				_navigationService = navigationService;
				OnNavigationEnabled();
			}
		}

		internal void RaiseNavigatedFrom(NavigationEventArgs args)
		{
			OnNavigatedFrom(args);
		}

		internal void RaiseNavigatedTo(NavigationEventArgs args)
		{
			OnNavigatedTo(args);

			RaisePropertyChanged(() => CanGoBack);
		}

		/// <summary>
		/// Navigates to the most recent entry in back navigation history, if there is one.
		/// </summary>
		protected void GoBack()
		{
			EnsureNavigationServiceEnabled();
			_navigationService.GoBack();
		}

		/// <summary>
		/// Navigates to a default view associated with the specified <see cref="T:NavigatorViewModel">view model type. No parameter passed.</see>.
		/// </summary>
		protected void Navigate<TViewModel>()
			where TViewModel : NavigableViewModelBase
		{
			Navigate<TViewModel>(NavigableAttribute.DefaultTarget, null);
		}

		/// <summary>
		/// Navigates to a view associated with the specified <see cref="T:NavigatorViewModel">view model type</see>.
		/// </summary>
		/// <param name="target">The target view name.</param>
		/// <param name="parameter">The context view model.</param>
		protected void Navigate<TViewModel>(string target, object parameter)
			where TViewModel : NavigableViewModelBase
		{
			string ensuredTarget = String.IsNullOrEmpty(target) ? NavigableAttribute.DefaultTarget : target;

			EnsureNavigationServiceEnabled();
			_navigationService.Navigate<TViewModel>(ensuredTarget, parameter);
		}

		/// <summary>
		/// Navigates to a view associated with the specified <see cref="T:NavigatorViewModel">view model type</see>.
		/// Binds view model to the view
		/// </summary>
		/// <param name="target">The target view name.</param>
		/// <param name="parameter">The context view model.</param>
		protected void Navigate<TViewModel>(string target, TViewModel viewModel, object parameter)
			where TViewModel : NavigableViewModelBase
		{
			string ensuredTarget = String.IsNullOrEmpty(target) ? NavigableAttribute.DefaultTarget : target;

			EnsureNavigationServiceEnabled();
			_navigationService.Navigate<TViewModel>(ensuredTarget, viewModel,  parameter);
		}

		/// <summary>
		/// Called when the view is navigated from.
		/// </summary>
		/// <param name="args">The <see cref="NavigationEventArgs" /> instance containing the event data.</param>
		protected virtual void OnNavigatedFrom(NavigationEventArgs args)
		{
		}

		/// <summary>
		/// Called when the view is navigated to.
		/// </summary>
		/// <param name="args">The <see cref="NavigationEventArgs" /> instance containing the event data.</param>
		protected virtual void OnNavigatedTo(NavigationEventArgs args)
		{
		}

		/// <summary>
		/// Raises the <see cref="E:NavigationEnabled" /> event.
		/// </summary>
		/// <param name="eventArgs">The <see cref="EventArgs" /> instance containing the event data.</param>
		protected virtual void OnNavigationEnabled()
		{
			EventHandler navigationEnabled = this.NavigationEnabled;
			if(navigationEnabled != null)
				navigationEnabled(this, EventArgs.Empty);
		}

		/// <summary>
		/// Use to prohibit navigating back to certain pages like authentication
		/// </summary>
		protected void ClearNavigationHistory()
		{
			EnsureNavigationServiceEnabled();
			_navigationService.ClearNavigationHistory();
		}


		private void EnsureNavigationServiceEnabled()
		{
			if(_navigationService == null)
				throw new InvalidOperationException();
		}
	}
}