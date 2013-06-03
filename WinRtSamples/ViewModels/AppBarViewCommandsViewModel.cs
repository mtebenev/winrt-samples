using System.Windows.Input;
using Mt.Common.UiCore.MvvmCore;
using Mt.WinRtSamples.Views;

namespace Mt.WinRtSamples.ViewModels
{
	/// <summary>
	/// View model provides view-specific commands on shared AppBar
	/// </summary>
	[Navigable(typeof(AppBarViewCommandsPage))]
	public class AppBarViewCommandsViewModel : DemoViewModelBase
	{
		public AppBarViewCommandsViewModel()
		{
			// Init commands.
			ViewSpecificCommand1 = new DelegateCommand(HandleViewSpecificCommand1);
			ViewSpecificCommand2 = new DelegateCommand(HandleViewSpecificCommand2);
		}

		public override string Title
		{
			get { return "View-specific AppBar commands"; }
		}

		public override string Description
		{
			get { return "Demonstrates approach for declaring view-specific commands in AppBar with MVVM approach."; }
		}

		public ICommand ViewSpecificCommand1 { get; private set; }
		public ICommand ViewSpecificCommand2 { get; private set; }

		private void HandleViewSpecificCommand2()
		{
		}

		private void HandleViewSpecificCommand1()
		{
		}
	}
}
