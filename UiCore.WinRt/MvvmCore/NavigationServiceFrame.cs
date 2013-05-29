using System;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Mt.Common.UiCore.MvvmCore
{
	/// <summary>
	/// Frame-based implementation for the <see cref="INavigationService"/> interface.
	/// </summary>
	partial class NavigationServiceFrame : INavigationService
	{
		private readonly Frame _frame;
		private readonly NavigableViewModelBase _navigableViewModel;

		/// <summary>
		/// Initializes a new instance of the <see cref="NavigationServiceFrame"/> class.
		/// </summary>
		/// <param name="navigableViewModel">The navigator view model.</param>
		/// <param name="frame">The navigation service.</param>
		internal NavigationServiceFrame(NavigableViewModelBase navigableViewModel, Frame frame)
		{
			_navigableViewModel = navigableViewModel;
			_frame = frame;
		}

		/// <summary>
		/// Gets a value that indicates whether there is at least one entry in back navigation history.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if there is at least one entry in back navigation history; otherwise, <c>false</c>.
		/// </value>
		public bool CanGoBack
		{
			get
			{
				return _frame.CanGoBack;
			}
		}

		/// <summary>
		/// Navigates to the most recent entry in back navigation history, if there is one.
		/// </summary>
		public void GoBack()
		{
			SubscribeNavigatedHandler(null);
			_frame.GoBack();
		}

		/// <summary>
		/// Navigates to a view associated with the specified <see cref="T:NavigatorViewModel">context</see>.
		/// </summary>
		/// <param name="target">The target view name.</param>
		/// <param name="parameter">Parameter to be passed to OnNavigatedTo</param>
		public void Navigate<TViewModel>(string target, object parameter)
			where TViewModel : NavigableViewModelBase
		{
			SubscribeNavigatedHandler(null);

			Type pageType = GetPageType(target, typeof(TViewModel));
			_frame.Navigate(pageType, parameter);
		}

		/// <summary>
		/// Navigates to a view associated with the specified <see cref="T:NavigatorViewModel">context</see>.
		/// </summary>
		/// <param name="target">The target view name.</param>
		/// <param name="viewModel">ViewModel to be bound with the view</param>
		/// <param name="parameter">Parameter to be passed to OnNavigatedTo</param>
		public void Navigate<TViewModel>(string target, TViewModel viewModel, object parameter)
			where TViewModel : NavigableViewModelBase
		{
			SubscribeNavigatedHandler(viewModel);

			Type pageType = GetPageType(target, typeof(TViewModel));
			_frame.Navigate(pageType, parameter);
		}

		/// <summary>
		/// Navigates to default target of the view model and binds view model to the view
		/// </summary>
		public void Navigate<TViewModel>(TViewModel viewModel) where TViewModel : NavigableViewModelBase
		{
			SubscribeNavigatedHandler(viewModel);

			Type pageType = GetPageType(null, typeof(TViewModel));
			_frame.Navigate(pageType, null);
		}

		public void ClearNavigationHistory()
		{
			// "1,0" is what have Frame when navigation history is empty
			_frame.SetNavigationState("1,0");
		}

		/// <summary>
		/// Handles the Navigated event.
		/// </summary>
		/// <param name="viewModelToBind">ViewModel to be bound to the target view or null</param>
		private void SubscribeNavigatedHandler(NavigableViewModelBase viewModelToBind)
		{
			NavigatedEventHandler navigatedHandler = null;

			navigatedHandler = (sender, e) =>
			{
				_frame.Navigated -= navigatedHandler;
				HandleServiceNavigatedToContent(e, viewModelToBind);
			};

			_frame.Navigated += navigatedHandler;
		}

		private void HandleServiceNavigatedToContent(NavigationEventArgs e, NavigableViewModelBase viewModelToBind)
		{
			_navigableViewModel.RaiseNavigatedFrom(e);

			// Raise navigated to viewmodel event if the target view defines navigable view model
			FrameworkElement element = e.Content as FrameworkElement;
			if(element != null)
			{
				RoutedEventHandler elementLoadedHandler = null;
				elementLoadedHandler = delegate
				{
					element.Loaded -= elementLoadedHandler;

					if(viewModelToBind != null)
						element.DataContext = viewModelToBind;

					// Enable navigation for the viewmodel


					// Raise navigated to
					NavigableViewModelBase navigableViewModel = element.DataContext as NavigableViewModelBase; // ViewModel may be sent by the view and viewModelToBind == true
					if(navigableViewModel != null)
					{
						NavigationServiceFrame navigationService = new NavigationServiceFrame(navigableViewModel, _frame);
						navigableViewModel.EnableNavigation(navigationService);

						navigableViewModel.RaiseNavigatedTo(e);
					}
				};

				element.Loaded += elementLoadedHandler;
			}
		}

		/// <summary>
		/// Gets the page URI by a target view name.
		/// </summary>
		/// <param name="target">The target view name.</param>
		/// <param name="contextType">The context view model.</param>
		private static Type GetPageType(string target, Type contextType)
		{
			NavigableAttribute navigableAttr = contextType
				.GetTypeInfo()
				.GetCustomAttributes(typeof(NavigableAttribute), inherit: true)
				.Cast<NavigableAttribute>()
				.SingleOrDefault(attr => attr.Target == target);

			if(navigableAttr == null)
				throw new InvalidOperationException();

			return navigableAttr.PageType;
		}
	}
}