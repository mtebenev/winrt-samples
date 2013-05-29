using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mt.Common.UiCore.MvvmCore
{
	/// <summary>
	/// The class holds context of async operations being executed. Use in conjunction with BusyIndicator to keep the indicator during WCF and other async calls.
	/// </summary>
	public class AsyncOperationContext : NotificationObject
	{
		private readonly IList<AsyncOperation> _operations;

		public AsyncOperationContext()
		{
			_operations = new List<AsyncOperation>();
		}

		/// <summary>
		/// Indicates that application currently busy (some async operation is running under busy indicator)
		/// </summary>
		public bool IsBusy
		{
			get
			{
				bool result = (_operations.Count > 0);
				return result;
			}
		}

		/// <summary>
		/// Message for busy indicator
		/// </summary>
		public string BusyMessage
		{
			get
			{
				string result = string.Empty;

				// Get busy message string from the last operation
				if(_operations.Count > 0)
					result = _operations[_operations.Count - 1].BusyMessage;

				return result;
			}
		}

		/// <summary>
		/// Full-spec WCF call with context message, complete/error handlers
		/// </summary>
		public async Task<T> Invoke<T>(Task<T> task, string waitMsg)
		{
			AsyncOperation operation = new AsyncOperation(waitMsg);
			T result;

			try
			{
				StartOperation(operation);
				result = await task;
			}
			finally
			{
				StopOperation(operation);
			}

			return result;
		}

		/// <summary>
		/// Full-spec WCF call with context message, complete/error handlers (no result)
		/// </summary>
		public async Task Invoke(Task task, string waitMsg)
		{
			AsyncOperation operation = new AsyncOperation(waitMsg);

			try
			{
				StartOperation(operation);
				await task;
			}
			finally
			{
				StopOperation(operation);
			}
		}

		/// <summary>
		/// Invokes operation call with default wait message
		/// </summary>
		public async Task<T> Invoke<T>(Task<T> task)
		{
			return await Invoke(task, null);
		}

		/// <summary>
		/// Invokes operation call with default wait message (no result)
		/// </summary>
		public async Task Invoke(Task task)
		{
			await Invoke(task, null);
		}

		/// <summary>
		/// Registers an operation in the service
		/// </summary>
		public void StartOperation(AsyncOperation asyncOperation)
		{
			_operations.Add(asyncOperation);

			RaisePropertyChanged(() => IsBusy);
			RaisePropertyChanged(() => BusyMessage);
		}

		/// <summary>
		/// Stops the wait panel UI for the operation
		/// </summary>
		public void StopOperation(AsyncOperation asyncOperation)
		{
			_operations.Remove(asyncOperation);

			RaisePropertyChanged(() => IsBusy);
			RaisePropertyChanged(() => BusyMessage);
		}
	}
}
