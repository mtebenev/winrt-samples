using System.Threading.Tasks;

namespace Mt.Common.UiCore.MvvmCore
{
	/// <summary>
	/// Async GUI-related task extensions
	/// </summary>
	public static class TaskExtensions
	{
		public static async Task<T> ExecuteOnOperationContext<T>(this Task<T> task, AsyncOperationContext operationContext)
		{
			return await operationContext.Invoke(task);
		}

		public static async Task ExecuteOnOperationContext(this Task task, AsyncOperationContext operationContext)
		{
			await operationContext.Invoke(task);
		}
	}
}
