using System;

namespace Mt.Common.UiCore.Core
{
	/// <summary>
	/// The app-wide interface is responsible for handling errors (logging them and providing to users)
	/// </summary>
	public interface IAppErrorHandler
	{
		/// <summary>
		/// Adds another error to the handler.
		/// Pass true to showExceptionDetails to display message provided by exception.
		/// Otherwise handler will show generic message.
		/// </summary>
		void AddErrorInfo(Exception exception, bool showExceptionDetails);

		/// <summary>
		/// Shortcut message: not uses exception message
		/// </summary>
		void AddErrorInfo(Exception exception);
	}
}