using System.Diagnostics;

namespace Mt.Common.WinRtAppCore.Logging
{
	/// <summary>
	/// Primitive logger implementation utilizing VS trace
	/// </summary>
	internal class LoggerTrace : ILogger
	{
		public void Info(string message)
		{
			Debug.WriteLine(message);
		}
	}
}