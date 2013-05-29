namespace Mt.Common.UiCore.MvvmCore
{
	/// <summary>
	/// Enables <see cref="T:ViewModel">view models</see> to support and reflect view navigation.
	/// </summary>
	public interface INavigationService
	{
		/// <summary>
		/// Gets a value that indicates whether there is at least one entry in back navigation history.
		/// </summary>
		/// <value>
		///		<c>true</c> if there is at least one entry in back navigation history; otherwise, <c>false</c>.
		/// </value>
		bool CanGoBack { get; }

		/// <summary>
		/// Navigates to the most recent entry in back navigation history, if there is one.
		/// </summary>
		void GoBack();

		/// <summary>
		/// Navigates to a view associated with the specified <see cref="T:NavigatorViewModel">context</see>.
		/// </summary>
		/// <param name="target">The target view name.</param>
		/// <param name="parameter">The context view model.</param>
		void Navigate<TViewModel>(string target, object parameter) where TViewModel : NavigableViewModelBase;

		/// <summary>
		/// Navigates to a view associated with the specified <see cref="T:NavigatorViewModel">context</see>.
		/// </summary>
		/// <param name="target">The target view name.</param>
		/// <param name="viewModel">ViewModel to be bound with the view</param>
		/// <param name="parameter">Parameter to be passed to OnNavigatedTo</param>
		void Navigate<TViewModel>(string target, TViewModel viewModel, object parameter) where TViewModel : NavigableViewModelBase;

		/// <summary>
		/// Navigates to a view associated with the specified <see cref="T:NavigatorViewModel">context</see>.
		/// </summary>
		/// <param name="target">The target view name.</param>
		/// <param name="viewModel">ViewModel to be bound with the view</param>
		/// <param name="parameter">Parameter to be passed to OnNavigatedTo</param>
		void Navigate<TViewModel>(TViewModel viewModel) where TViewModel : NavigableViewModelBase;

		/// <summary>
		/// Clears navigation history so user cannot navigate back
		/// </summary>
		void ClearNavigationHistory();
	}
}