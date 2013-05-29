using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Mt.Common.UiCore.Converters
{
	/// <summary>
	/// Converts to true if a given value matched specified value
	/// Useful in binding enumerations to radio button group
	/// </summary>
	public class EnumToBooleanConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			string parameterString = parameter as string;
			if(String.IsNullOrEmpty(parameterString))
				return DependencyProperty.UnsetValue;

			if(Enum.IsDefined(value.GetType(), value) == false)
				return DependencyProperty.UnsetValue;

			object paramvalue = Enum.Parse(value.GetType(), parameterString, false);

			bool result = paramvalue.Equals(value);
			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			string parameterString = parameter as string;

			object result = DependencyProperty.UnsetValue;
			if(!string.IsNullOrEmpty(parameterString) && !value.Equals(false))
				result = Enum.Parse(targetType, parameterString, false);

			return result;
		}
	}
}