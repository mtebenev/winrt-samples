using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Mt.Common.AppCore.Utils;

namespace Mt.Common.UiCore.MvvmCore
{
	/// <summary>
	/// Base class for view models with validation support. Automatically uses standard validation attributes from DataAnnotations (the preferred way)
	/// You can add validators manually as well
	/// </summary>
	/// <typeparam name="TBindingModel">(View)Model with bindable properties</typeparam>
	public class ValidatingViewModelBase<TBindingModel> : NotificationObject, INotifyDataErrorInfo
		where TBindingModel : ValidatingViewModelBase<TBindingModel>
	{
		public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = delegate { };

		private readonly List<PropertyValidationRule<TBindingModel>> _validations = new List<PropertyValidationRule<TBindingModel>>();
		private Dictionary<string, List<string>> _errorMessages = new Dictionary<string, List<string>>();

		/// <summary>
		/// Constructor (with automatically validate properties on change)
		/// </summary>
		protected ValidatingViewModelBase()
			: this(true)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="isAutoValidateProperties">Pass true to automatically validate properties on change</param>
		protected ValidatingViewModelBase(bool isAutoValidateProperties)
		{
			if(isAutoValidateProperties)
			{
				PropertyChanged += (s, e) =>
				{
					if(e.PropertyName != PropertySupport.ExtractPropertyName(() => HasErrors))
						ValidateProperty(e.PropertyName);
				};
			}

			AddAllAttributeValidators();
		}

		/// <summary>
		/// Call to add specific validator to a model property
		/// </summary>
		protected PropertyValidationRule<TBindingModel> AddValidationFor<T>(Expression<Func<T>> propertyExpression, ValidationAttribute validator = null)
		{
			string propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
			PropertyValidationRule<TBindingModel> result = AddValidationFor<T>(propertyName, validator);

			return result;
		}

		protected PropertyValidationRule<TBindingModel> AddValidationFor<T>(string propertyName, ValidationAttribute validator = null)
		{
			PropertyValidationRule<TBindingModel> validation = new PropertyValidationRule<TBindingModel>(propertyName, validator);
			_validations.Add(validation);

			return validation;
		}

		protected void AddAttributeValidatorFor<T>(Expression<Func<T>> propertyExpression, ValidationAttribute validationAttribute)
		{
			MemberExpression memberExpression = propertyExpression.Body as MemberExpression;

			if(memberExpression == null)
				throw new ArgumentException("The expression is not a member access expression", "propertyExpression");

			PropertyInfo property = memberExpression.Member as PropertyInfo;

			if(property == null)
				throw new ArgumentException("The member access expression does not access a property", "propertyExpression");

			AddAttributeValidator(property, validationAttribute);
		}

		public IEnumerable GetErrors(string propertyName)
		{
			if(_errorMessages.ContainsKey(propertyName))
				return _errorMessages[propertyName];

			return new string[0];
		}

		public virtual bool HasErrors
		{
			get
			{
				return _errorMessages.Count > 0;
			}
		}

		/// <summary>
		/// Call to validate all properties registered for validation at once
		/// </summary>
		public void ValidateAll()
		{
			OnValidating();

			Dictionary<string, List<string>>.KeyCollection propertyNamesWithValidationErrors = _errorMessages.Keys;
			ClearAllErrorMessages();

			_validations.ForEach(PerformValidation);
			List<string> propertyNamesThatMightHaveChangedValidation = _errorMessages.Keys.Union(propertyNamesWithValidationErrors).ToList();
			propertyNamesThatMightHaveChangedValidation.ForEach(OnErrorsChanged);

			RaisePropertyChanged(() => HasErrors);
		}

		/// <summary>
		/// Inherited class can override to process children validation
		/// </summary>
		protected virtual void OnValidating()
		{
		}

		/// <summary>
		/// Call to clear all validation message for the model
		/// </summary>
		public void ResetValidationState()
		{
			OnResettingValidationState();
			Dictionary<string, List<string>>.KeyCollection propertyNamesWithValidationErrors = _errorMessages.Keys;
			ClearAllErrorMessages();

			propertyNamesWithValidationErrors.ForEach(OnErrorsChanged);
			RaisePropertyChanged(() => HasErrors);
		}

		/// <summary>
		/// Inherited class can override to process children state reset
		/// </summary>
		protected virtual void OnResettingValidationState()
		{
		}

		/// <summary>
		/// Validate specific property
		/// </summary>
		/// <param name="propertyExpression">Path to property</param>
		/// <param name="ignoreValidatorsPredicate">Predicate for filtering validators. Validator will be ignored, if predicate returns true. Can be null - in this case all validators will work.</param>
		public void ValidateProperty<T>(Expression<Func<T>> propertyExpression, Func<PropertyValidationRule<TBindingModel>, bool> ignoreValidatorsPredicate = null)
		{
			ValidateProperty(PropertySupport.ExtractPropertyName(propertyExpression), ignoreValidatorsPredicate);
		}

		/// <summary>
		/// Validate specific property
		/// </summary>
		/// <param name="propertyName">Property name to validate</param>
		/// <param name="ignoreValidatorsPredicate">Predicate for filtering validators. Validator will be ignored, if predicate returns true. Can be null - in this case all validators will work.</param>
		private void ValidateProperty(string propertyName, Func<PropertyValidationRule<TBindingModel>, bool> ignoreValidatorsPredicate = null)
		{
			ClearErrorMessagesForProperty(propertyName);

			IEnumerable<PropertyValidationRule<TBindingModel>> validations = _validations;

			// If view model wants to ignore some validators, she can exclude some validators from validation process
			if(ignoreValidatorsPredicate != null)
			{
				// if predicate returns true, this validator will be ignored
				validations = validations.Where(v => ignoreValidatorsPredicate(v) == false);
			}

			validations
				.Where(v => v.PropertyName == propertyName)
				.ToArray()
				.ForEach(PerformValidation);

			OnErrorsChanged(propertyName);
			RaisePropertyChanged(() => HasErrors);
		}

		/// <summary>
		/// Clear validation messages for specific property
		/// </summary>
		/// <param name="propertyExpression">Path to property</param>
		public void ClearValidationMessage<T>(Expression<Func<T>> propertyExpression)
		{
			string propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
			ClearValidationMessage(propertyName);
		}

		/// <summary>
		/// Clear validation messages for specific property
		/// </summary>
		/// <param name="propertyName">Property name to validate</param>
		public void ClearValidationMessage(string propertyName)
		{
			ClearErrorMessagesForProperty(propertyName);

			OnErrorsChanged(propertyName);
			RaisePropertyChanged(() => HasErrors);
		}

		private void OnErrorsChanged(string propertyName)
		{
			ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
		}

		private void PerformValidation(PropertyValidationRule<TBindingModel> validation)
		{
			TBindingModel presentationModel = (TBindingModel)this;

			if(validation.IsInvalid(presentationModel))
				AddErrorMessageForProperty(validation.PropertyName, validation.GetErrorMessage(presentationModel));
		}

		private void AddErrorMessageForProperty(string propertyName, string errorMessage)
		{
			if(_errorMessages.ContainsKey(propertyName))
				_errorMessages[propertyName].Add(errorMessage);
			else
				_errorMessages.Add(propertyName, new List<string> { errorMessage });
		}

		private void ClearAllErrorMessages()
		{
			_errorMessages = new Dictionary<string, List<string>>();
		}

		private void ClearErrorMessagesForProperty(string propertyName)
		{
			_errorMessages.Remove(propertyName);
		}

		/// <summary>
		/// Adds validators from associated attributes for all properties in the model
		/// </summary>
		private void AddAllAttributeValidators()
		{
			PropertyInfo[] propertyInfos = typeof(TBindingModel)
				.GetRuntimeProperties()
				.Where(p => p.CanRead && p.CanWrite)
				.ToArray();


			foreach(PropertyInfo propertyInfo in propertyInfos)
			{
				ValidationAttribute[] custom = propertyInfo.GetCustomAttributes<ValidationAttribute>(true)
					.ToArray();

				foreach(ValidationAttribute attribute in custom)
					AddAttributeValidator(propertyInfo, attribute);
			}
		}

		private void AddAttributeValidator(PropertyInfo propertyInfo, ValidationAttribute validationAttribute)
		{
			string name = propertyInfo.Name;

			DisplayAttribute displayAttribute = propertyInfo.GetCustomAttributes<DisplayAttribute>().FirstOrDefault();
			if(displayAttribute != null)
				name = displayAttribute.GetName();

			// Note: See localization guidelines to properly localize the message
			ValidationResult validationResult = null;

			AddValidationFor<TBindingModel>(propertyInfo.Name, validationAttribute)
				.When(
				x =>
				{
					object value = propertyInfo.GetMethod.Invoke(this, new object[] { });
					ValidationContext validationContext =
						new ValidationContext(this)
						{
							MemberName = propertyInfo.Name
						};

					validationResult = validationAttribute.GetValidationResult(value, validationContext);
					return validationResult != ValidationResult.Success;
				})
				.ShowMessage(
				(validatingModel) =>
				{
					string result;

					if(validationResult != null && !String.IsNullOrEmpty(validationResult.ErrorMessage))
						result = validationResult.ErrorMessage;
					else
						result = validationAttribute.FormatErrorMessage(name);

					return result;
				});
		}
	}
}
