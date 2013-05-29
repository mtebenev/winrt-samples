using System;
using Windows.UI.Xaml.Data;

namespace Mt.Common.UiCore.Converters
{
	/// <summary>
	/// Negates boolean value
	/// </summary>
	public class NotConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			object result = null;

			if(targetType == typeof(bool))
			{
				result = true;

				if(value != null)
					result = !((bool) value);
			}

			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			if(value is bool && targetType == typeof(bool))
				return !((bool)value);

			throw new NotSupportedException();
		}
	}
}
