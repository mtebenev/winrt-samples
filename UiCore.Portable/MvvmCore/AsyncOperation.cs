namespace Mt.Common.UiCore.MvvmCore
{
	/// <summary>
	/// Contains an info about an async operation performed in GUI.
	/// Used to display progress GUI when application performs a long-running background tasks.
	/// </summary>
	public class AsyncOperation
	{
		public AsyncOperation(string busyMessage)
		{
			BusyMessage = busyMessage;
		}

		/// <summary>
		/// Message displayed in GUI (usually busy indicator)
		/// </summary>
		public string BusyMessage { get; private set; }
	}
}