namespace Mt.Common.WinRtAppCore.Logging
{
	/// <summary>
	/// Provides logger interface to clients
	/// </summary>
	public static class LoggerService
	{
		public static ILogger GetLogger()
		{
			return new LoggerTrace();
		}
	}
}
