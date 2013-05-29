using System.ComponentModel.DataAnnotations;
using Mt.Common.UiCore.MvvmCore;

namespace Mt.WinRtSamples.ViewModels
{
	/// <summary>
	/// Form model with validation support
	/// </summary>
	public class FormPaneModel : ValidatingViewModelBase<FormPaneModel>
	{
		private string _firstName;
		private string _lastName;
		private int _age;

		[Display(Name = "First Name")]
		[Required]
		public string FirstName
		{
			get { return _firstName; }
			set { SetProperty(ref _firstName, value); }
		}

		[Display(Name = "Last Name")]
		[Required]
		public string LastName
		{
			get { return _lastName; }
			set { SetProperty(ref _lastName, value); }
		}

		[Range(18, 55)]
		public int Age
		{
			get { return _age; }
			set { SetProperty(ref _age, value); }
		}
	}
}
