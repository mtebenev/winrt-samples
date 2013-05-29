using Mt.Common.UiCore.MvvmCore;

namespace Mt.WinRtSamples.ViewModels
{
	/// <summary>
	/// Base class for all demo view in the application
	/// </summary>
	public abstract class DemoViewModelBase : NavigableViewModelBase
	{
		/// <summary>
		/// Returns title of the view
		/// </summary>
		public abstract string Title { get; }

		/// <summary>
		/// Short hint for the view
		/// </summary>
		public abstract string Description { get; }
	}
}