using System;

namespace Mt.Common.UiCore.Converters
{
	/// <summary>
	/// Enum-specific converter.
	/// </summary>
	public class EnumKeyToValueConverter : KeyToValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, string language)
		{
			string key = Enum.GetName(value.GetType(), value);
			return base.Convert(key, targetType, parameter, language);
		}

		public override object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			string key = (string)base.ConvertBack(value, typeof(String), parameter, language);
			return Enum.Parse(targetType, key, false);
		}
	}
}