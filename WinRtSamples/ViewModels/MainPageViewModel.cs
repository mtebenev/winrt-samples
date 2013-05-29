using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Mt.Common.UiCore.MvvmCore;

namespace Mt.WinRtSamples.ViewModels
{
	/// <summary>
	/// View model for the main page
	/// </summary>
	public class MainPageViewModel : NavigableViewModelBase
	{
		private readonly IList<DemoViewModelBase> _demoViewModels;

		public MainPageViewModel()
		{
			_demoViewModels = 
				new List<DemoViewModelBase>
					{
						new LayoutBehaviorViewModel(),
						new ValidationViewModel(),
						new IncrementalSourceViewModel()
					};

			NavigateCommand = new DelegateCommand<DemoViewModelBase>(HandleNavigateCommand);
		}

		public IEnumerable<DemoViewModelBase> DemoViewModels
		{
			get
			{
				return _demoViewModels;
			}
		}

		public ICommand NavigateCommand { get; private set; }

		private void HandleNavigateCommand(DemoViewModelBase demoViewModel)
		{
			// Invoke generic through view model type
			MethodInfo navigateMethod = this.GetType()
				.GetRuntimeMethods()
				.Single(m => m.Name == "Navigate" && m.GetParameters().Length == 0 && m.ContainsGenericParameters);

			MethodInfo genericMethod = navigateMethod.MakeGenericMethod(new Type[] {demoViewModel.GetType()});
			genericMethod.Invoke(this, new object[] {});
		}
	}
}
