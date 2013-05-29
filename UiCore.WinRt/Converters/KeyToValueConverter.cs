using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Markup;

namespace Mt.Common.UiCore.Converters
{
	/// <summary>
	/// Converts a key (some object convertible to string) to a value defined in resource dictionary)
	/// </summary>
	[ContentProperty(Name = "Items")]
	public class KeyToValueConverter : IValueConverter
	{
		public ResourceDictionary Items { get; set; }
		public object DefaultValue { get; set; }

		public virtual object Convert(object value, Type targetType, object parameter, string language)
		{
			object result;

			if(value == null || !Items.TryGetValue(value.ToString(), out result))
				result = DefaultValue;

			return result;
		}

		public virtual object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			return Items.FirstOrDefault(kvp => value.Equals(kvp.Value)).Key;
		}
	}
}
