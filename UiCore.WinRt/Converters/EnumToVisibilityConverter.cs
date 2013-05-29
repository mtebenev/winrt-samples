using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Mt.Common.UiCore.Converters
{
	/// <summary>
	/// Converts to Visibility.Visible if a given value matched specified value
	/// Useful in binding enumerations to radio button group
	/// </summary>
	public class EnumToVisibilityConverter : IValueConverter
	{
		private readonly EnumToBooleanConverter _enumToBooleanConverter;

		public EnumToVisibilityConverter()
		{
			_enumToBooleanConverter = new EnumToBooleanConverter();
		}

		public object Convert(object value, Type targetType, object parameter, string language)
		{
			bool result = (bool)_enumToBooleanConverter.Convert(value, targetType, parameter, language);
			return result ? Visibility.Visible : Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			return _enumToBooleanConverter.ConvertBack((value is Visibility) && ((Visibility)value == Visibility.Visible), targetType, parameter, language);
		}
	}
}