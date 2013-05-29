using System.Windows.Input;
using Mt.Common.UiCore.MvvmCore;
using Mt.WinRtSamples.Views;

namespace Mt.WinRtSamples.ViewModels
{
	/// <summary>
	/// View model for validation sample (aggregates form)
	/// </summary>
	[Navigable(typeof(ValidationView))]
	public class ValidationViewModel : DemoViewModelBase
	{
		private readonly FormPaneModel _formModel;

		public ValidationViewModel()
		{
			_formModel = new FormPaneModel();
			SubmitCommand = new DelegateCommand(HandleSubmitCommand);
		}

		public override string Title
		{
			get { return "Form Validation"; }
		}

		public override string Description
		{
			get { return "Validate form fields with INotifyDataErrorInfo."; }
		}

		public ICommand SubmitCommand { get; private set; }

		public FormPaneModel FormPaneModel
		{
			get
			{
				return _formModel;
			}
		}

		private void HandleSubmitCommand()
		{
			_formModel.ValidateAll();
		}
	}
}
