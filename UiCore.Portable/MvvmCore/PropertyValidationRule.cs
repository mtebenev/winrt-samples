using System;
using System.ComponentModel.DataAnnotations;

namespace Mt.Common.UiCore.MvvmCore
{
	/// <summary>
	/// Encapsulates all necessary information about a single validation rule for a property
	/// </summary>
	/// <typeparam name="TBindingModel"></typeparam>
	public class PropertyValidationRule<TBindingModel>
		where TBindingModel : ValidatingViewModelBase<TBindingModel>
	{
		private string _errorMessage;
		private readonly string _propertyName;

		private Func<TBindingModel, bool> _validationCriteria;
		private Func<TBindingModel, string> _errorMessageFunc;
	
		private readonly ValidationAttribute _validationAttribute;

		public PropertyValidationRule(string propertyName, ValidationAttribute validator)
		{
			_propertyName = propertyName;
			_validationAttribute = validator; // Storing validation attribute to be able to check its type in ignoring predicate
		}

		public PropertyValidationRule<TBindingModel> ShowMessage(string errorMessage)
		{
			if (_errorMessage != null || _errorMessageFunc != null)
				throw new InvalidOperationException("You can only set the message once.");

			_errorMessage = errorMessage;
			return this;
		}

		public PropertyValidationRule<TBindingModel> ShowMessage(Func<TBindingModel, string> errorMessageFunc)
		{
			if (_errorMessage != null || _errorMessageFunc != null)
				throw new InvalidOperationException("You can only set the message once.");

			_errorMessageFunc = errorMessageFunc;
			return this;
		}

		public PropertyValidationRule<TBindingModel> When(Func<TBindingModel, bool> validationCriteria)
		{
			if (_validationCriteria != null)
				throw new InvalidOperationException("You can only set the validation criteria once.");

			_validationCriteria = validationCriteria;
			return this;
		}

		public bool IsInvalid(TBindingModel presentationModel)
		{
			if (_validationCriteria == null)
				throw new InvalidOperationException("No criteria have been provided for this validation. (Use the 'When(..)' method.)");

			return _validationCriteria(presentationModel);
		}

		public string GetErrorMessage(TBindingModel presentationModel)
		{
			if (_errorMessage == null && _errorMessageFunc == null)
				throw new InvalidOperationException("No error message has been set for this validation. (Use the 'ShowMessage(..)' method.)");

			return _errorMessage ?? _errorMessageFunc(presentationModel);
		}

		public string PropertyName
		{
			get { return _propertyName; }
		}

		public ValidationAttribute ValidationAttribute
		{
			get { return _validationAttribute; }
		}
	}
}