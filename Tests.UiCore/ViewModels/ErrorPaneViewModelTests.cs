using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mt.Common.UiCore.ViewModels;

// ReSharper disable InconsistentNaming
namespace Mt.Common.Tests.UiCore.ViewModels
{
	/// <summary>
	/// Tests for common errors pane view model
	/// </summary>
	[TestClass]
	public class ErrorPaneViewModelTests
	{
		/// <summary>
		/// View model must display generic message if needed
		/// </summary>
		[TestMethod]
		public void Generic_Message_Must_Be_Displayed()
		{
			const string genericMessage = "generic_message";
			ErrorInfoPaneModel viewModel = new ErrorInfoPaneModel(genericMessage);

			Exception exception = new Exception("specific_message");
			viewModel.AddErrorInfo(exception, false);

			Assert.AreEqual(genericMessage, viewModel.ErrorDescription);
		}

		/// <summary>
		/// Display exception-specific message
		/// </summary>
		[TestMethod]
		public void Specific_Message_Must_Be_Displayed()
		{
			const string genericMessage = "generic_message";
			const string specificMessage = "specific_message";

			ErrorInfoPaneModel viewModel = new ErrorInfoPaneModel(genericMessage);

			Exception exception = new Exception(specificMessage);
			viewModel.AddErrorInfo(exception, true);

			Assert.AreEqual(specificMessage, viewModel.ErrorDescription);
		}
	}
}
