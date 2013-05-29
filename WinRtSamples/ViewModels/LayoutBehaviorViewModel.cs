using Mt.Common.UiCore.MvvmCore;
using Mt.WinRtSamples.Views;

namespace Mt.WinRtSamples.ViewModels
{
	[Navigable(typeof(LayoutBehaviorView))]
	public class LayoutBehaviorViewModel : DemoViewModelBase
	{
		public override string Title
		{
			get { return "Layout Transformation"; }
		}

		public override string Description
		{
			get { return "Perform layout transformation without inheritting LayoutAwarePage"; }
		}
	}
}
