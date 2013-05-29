namespace Mt.Common.WinRtAppCore.Logging
{
	/// <summary>
	/// Common logger client-side logger interface
	/// </summary>
	public interface ILogger
	{
		/// <summary>
		/// Writes an information message to log
		/// </summary>
		void Info(string message);
	}
}
