using Mt.WinRtSamples.ViewModels;
using Mt.WinRtSamples.Views;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace Mt.WinRtSamples
{
	/// <summary>
	/// Provides application-specific behavior to supplement the default Application class.
	/// </summary>
	sealed partial class App : Application
	{
		/// <summary>
		/// Initializes the singleton application object.  This is the first line of authored code
		/// executed, and as such is the logical equivalent of main() or WinMain().
		/// </summary>
		public App()
		{
			this.InitializeComponent();
			this.Suspending += OnSuspending;
		}

		/// <summary>
		/// Invoked when the application is launched normally by the end user.  Other entry points
		/// will be used when the application is launched to open a specific file, to display
		/// search results, and so forth.
		/// </summary>
		/// <param name="args">Details about the launch request and process.</param>
		protected override void OnLaunched(LaunchActivatedEventArgs args)
		{
			MainPage mainPage = Window.Current.Content as MainPage;

			// Do not repeat app initialization when the Window already has content,
			// just ensure that the window is active
			if(mainPage == null)
			{
				// Create a Frame to act as the navigation context and navigate to the first page
				MainPageViewModel mainPageViewModel = new MainPageViewModel();

				mainPage = new MainPage();
				mainPage.DataContext = mainPageViewModel;

				if(args.PreviousExecutionState == ApplicationExecutionState.Terminated)
				{
					//TODO: Load state from previously suspended application
				}

				// Place the frame in the current Window
				Window.Current.Content = mainPage;
			}

			/*
						if(mainPage.Content == null)
						{
							// When the navigation stack isn't restored navigate to the first page,
							// configuring the new page by passing required information as a navigation
							// parameter
							if(!mainPage.Navigate(typeof(MainPage), args.Arguments))
							{
								throw new Exception("Failed to create initial page");
							}
						}
			*/
			// Ensure the current window is active
			Window.Current.Activate();
		}

		/// <summary>
		/// Invoked when application execution is being suspended.  Application state is saved
		/// without knowing whether the application will be terminated or resumed with the contents
		/// of memory still intact.
		/// </summary>
		/// <param name="sender">The source of the suspend request.</param>
		/// <param name="e">Details about the suspend request.</param>
		private void OnSuspending(object sender, SuspendingEventArgs e)
		{
			var deferral = e.SuspendingOperation.GetDeferral();
			//TODO: Save application state and stop any background activity
			deferral.Complete();
		}
	}
}
