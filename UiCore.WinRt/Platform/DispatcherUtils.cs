using System;
using Windows.UI.Core;

namespace Mt.Common.UiCore.Platform
{
	/// <summary>
	/// WinRT-specific stuff for working with dispatcher
	/// </summary>
	public static class DispatcherUtils
	{
		/// <summary>
		/// Simple runner on dispatcher
		/// </summary>
		public static void Run(Action action)
		{
			CoreDispatcher dispatcher = Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher;
			dispatcher.RunAsync(CoreDispatcherPriority.Normal, new DispatchedHandler(action));
		}
	}
}
