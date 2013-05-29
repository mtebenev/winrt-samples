using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.Common.AppCore.Utils
{
	/// <summary>
	/// Various task-related utilities
	/// </summary>
	public static class TaskUtils
	{
		/// <summary>
		/// Shortcut for running an operation on current sync context
		/// </summary>
		public static Task RunOnCurrentContext(Func<Task> function)
		{
			return Task.Factory.StartNew(
				function,
				CancellationToken.None,
				TaskCreationOptions.None,
				TaskScheduler.FromCurrentSynchronizationContext());
		}

		/// <summary>
		/// Shortcut for running an operation on current sync context
		/// </summary>
		public static Task<T> RunOnCurrentContext<T>(Func<Task<T>> function)
		{
			return Task.Factory.StartNew(
				function,
				CancellationToken.None,
				TaskCreationOptions.None,
				TaskScheduler.FromCurrentSynchronizationContext())
				.Unwrap();
		}
	}
}
