using System;
using System.Collections.Generic;
using System.Windows.Input;
using Mt.Common.UiCore.Core;
using Mt.Common.UiCore.MvvmCore;

namespace Mt.Common.UiCore.ViewModels
{
	/// <summary>
	/// Error info pane provides user-friendly view to applicaiton errors
	/// </summary>
	public class ErrorInfoPaneModel : ViewModelBase, IAppErrorHandler
	{
		private readonly string _genericMessage;
		private readonly List<ErrorInfo> _errors;

		private class ErrorInfo
		{
			public ErrorInfo(Exception exception, bool showExceptionDetails)
			{
				Exception = exception;
				ShowExceptionDetails = showExceptionDetails;
			}

			public Exception Exception { get; private set; }
			public bool ShowExceptionDetails { get; private set; }
		}

		/// <summary>
		/// genericMessage will be used for exceptions that shouldn't be exposed in details to user (but still stored and reported if needed)
		/// </summary>
		public ErrorInfoPaneModel(string genericMessage)
		{
			_genericMessage = genericMessage;
			_errors = new List<ErrorInfo>();

			DismissMessageCommand = new DelegateCommand(HandleDismissMessageCommand);
			ReportErrorCommand = new DelegateCommand(HandleReportErrorCommand);
		}

		/// <summary>
		/// Returns true if there's some error registered and should be displayed
		/// </summary>
		public bool IsError
		{
			get
			{
				bool result = _errors.Count > 0;
				return result;
			}
		}

		public string ErrorDescription
		{
			get
			{
				string result = String.Empty;

				if(_errors.Count > 0)
					result =  _errors[0].ShowExceptionDetails ? _errors[0].Exception.Message : _genericMessage;

				return result;
			}
		}

		public void AddErrorInfo(Exception exception, bool showExceptionDetails)
		{
			ErrorInfo errorInfo = new ErrorInfo(exception, showExceptionDetails);
			_errors.Add(errorInfo);

			RaiseAllPublicPropertiesChanged();
		}

		public void AddErrorInfo(Exception exception)
		{
			AddErrorInfo(exception, false);
		}

		public ICommand DismissMessageCommand { get; private set; }
		public ICommand ReportErrorCommand { get; private set; }


		private void HandleReportErrorCommand()
		{
		}

		private void HandleDismissMessageCommand()
		{
			if(_errors.Count > 0)
			{
				_errors.RemoveAt(0);
				RaiseAllPublicPropertiesChanged();
			}
		}
	}
}
