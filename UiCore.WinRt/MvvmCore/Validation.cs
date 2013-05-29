using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Mt.Common.WinRtUiCore.MvvmCore
{
	/// <summary>
	/// Attached properties for enabling validation in views
	/// Based on: http://byteflux.me/implementing-inotifydataerrorinfo-validation-in-windows-8-winrt-xaml-applications/
	/// </summary>
	public static class Validation
	{
		public static readonly DependencyProperty PropertyProperty = 
			DependencyProperty.RegisterAttached("Property", typeof(string), typeof(Validation),
			new PropertyMetadata(null, OnValidationPropertyChanged));

		public static string GetProperty(DependencyObject obj)
		{
			return (string)obj.GetValue(PropertyProperty);
		}

		public static void SetProperty(DependencyObject obj, string value)
		{
			obj.SetValue(PropertyProperty, value);
		}

		public static readonly DependencyProperty ValidationPlaceholderProperty = 
			DependencyProperty.RegisterAttached("ValidationPlaceholder",  typeof(ContentControl), typeof(Validation),
			new PropertyMetadata(null, OnValidationPropertyChanged));

		public static ContentControl GetValidationPlaceholder(DependencyObject obj)
		{
			return (ContentControl)obj.GetValue(ValidationPlaceholderProperty);
		}

		public static void SetValidationPlaceholder(DependencyObject obj, ContentControl value)
		{
			obj.SetValue(ValidationPlaceholderProperty, value);
		}

		private static readonly DependencyProperty IsValidationAttachedProperty = 
			DependencyProperty.RegisterAttached("IsValidationAttached", typeof(bool), typeof(Validation),
			new PropertyMetadata(false, OnIsValidationAttachedChanged));

		private static void SetIsValidationAttached(DependencyObject element, bool value)
		{
			element.SetValue(IsValidationAttachedProperty, value);
		}

		private static bool GetIsValidationAttached(DependencyObject element)
		{
			return (bool)element.GetValue(IsValidationAttachedProperty);
		}

		public static readonly DependencyProperty DataContextProperty = 
			DependencyProperty.RegisterAttached("DataContext", typeof(object), typeof(Validation),
			new PropertyMetadata(null, OnDataContextChanged));

		private static void OnDataContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			INotifyDataErrorInfo context = e.OldValue as INotifyDataErrorInfo;

			if(context != null)
			{
				RemoveHandler(d, context);
				SetIsValidationAttached(d, false);
			}

			OnValidationPropertyChanged(d, e);
		}

		private static void RemoveHandler(DependencyObject dependencyObject, INotifyDataErrorInfo context)
		{
			EventHandler<DataErrorsChangedEventArgs> handler = GetEventHandler(dependencyObject);
			context.ErrorsChanged -= handler;
		}

		public static void SetDataContext(DependencyObject element, object value)
		{
			element.SetValue(DataContextProperty, value);
		}

		public static object GetDataContext(DependencyObject element)
		{
			return element.GetValue(DataContextProperty);
		}

		private static readonly DependencyProperty EventHandlerProperty =
				DependencyProperty.RegisterAttached("EventHandler", typeof(EventHandler<DataErrorsChangedEventArgs>), typeof(Validation), 
				new PropertyMetadata(default(EventHandler)));

		private static void SetEventHandler(DependencyObject element, EventHandler<DataErrorsChangedEventArgs> value)
		{
			element.SetValue(EventHandlerProperty, value);
		}

		private static EventHandler<DataErrorsChangedEventArgs> GetEventHandler(DependencyObject element)
		{
			return (EventHandler<DataErrorsChangedEventArgs>)element.GetValue(EventHandlerProperty);
		}

		private static void OnValidationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			// Check if validation is already attached to the property
			bool isValidationAttached = GetIsValidationAttached(d);
			if(isValidationAttached)
				return;

			string property = GetProperty(d);
			ContentControl placeholder = GetValidationPlaceholder(d);
			INotifyDataErrorInfo context = GetDataContext(d) as INotifyDataErrorInfo;

			if(!string.IsNullOrEmpty(property) && placeholder != null && context != null)
				SetIsValidationAttached(d, true);
		}

		private static void OnIsValidationAttachedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			FrameworkElement element = d as FrameworkElement;
			if(element == null)
				return;

			INotifyDataErrorInfo context = GetDataContext(d) as INotifyDataErrorInfo;

			if(context == null)
				return;

			ContentControl placeholder = GetValidationPlaceholder(d);
			string property = AddHandler(d, context, placeholder);

			UpdateValidationContent(property, context, placeholder);
		}

		private static string AddHandler(DependencyObject d, INotifyDataErrorInfo context, ContentControl placeholder)
		{
			string property = GetProperty(d);

			EventHandler<DataErrorsChangedEventArgs> contextOnErrorsChanged =
				(sender, args) =>
				{
					if(args.PropertyName == property)
						UpdateValidationContent(property, context, placeholder);
				};
			
			SetEventHandler(d, contextOnErrorsChanged);
			context.ErrorsChanged += contextOnErrorsChanged;
			
			return property;
		}

		private static void UpdateValidationContent(string propertyName, INotifyDataErrorInfo context, ContentControl validationPlaceholder)
		{
			IEnumerable<string> errors = context.GetErrors(propertyName).OfType<string>();
			validationPlaceholder.Content = errors.FirstOrDefault();
			validationPlaceholder.Visibility = errors.Any() ? Visibility.Visible : Visibility.Collapsed;
		}
	}
}