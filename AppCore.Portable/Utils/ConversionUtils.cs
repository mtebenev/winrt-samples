using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace Mt.Common.AppCore.Utils
{
	/// <summary>
	/// Various conversion helpers
	/// </summary>
	public static class ConversionUtils
	{
		private readonly static Type _stringType = typeof(string);

		/// <summary>
		/// Cast <paramref name="obj"/> value to type <typeparamref name="T"/>,
		/// if can't will throw exception
		/// </summary>
		public static T ToUnsafe<T>(this object obj)
		{
			return ParseObject(obj, default(T), true);
		}

		/// <summary>
		/// Try cast <paramref name = "obj" /> value to type <typeparamref name = "T" />,
		/// if can't will return default(<typeparamref name = "T" />)
		/// </summary>
		public static T To<T>(this object obj)
		{
			return To(obj, default(T));
		}

		/// <summary>
		/// Converts one enum to another if both have same members
		/// </summary>
		public static TNew ToEnum<TOld, TNew>(this TOld value, TNew defaultValue) where TNew : struct
		{
			TNew result;
			if(Enum.TryParse(value.ToString(), out result))
				return result;
			
			return defaultValue;
		}

		/// <summary>
		/// Try cast <paramref name = "obj" /> value to type <typeparamref name = "T" />,
		/// if can't will return <paramref name = "defaultValue" />
		/// </summary>
		public static T To<T>(this object obj, T defaultValue)
		{
			if(obj == null)
				return defaultValue;

			return ParseObject(obj, defaultValue, false);
		}

		private static T ParseObject<T>(object obj, T defaultValue, bool isUnsafe)
		{
			Type type = typeof(T);

			if(isUnsafe && obj == null)
				throw new ArgumentNullException("obj", string.Format(@"Can't convert '{0}' to type '{1}'.", obj, type));

			if(obj is T)
				return (T)obj;

			if(type == _stringType)
				return (T)(object)obj.ToString();

			Type underlyingType = Nullable.GetUnderlyingType(type);
			if(underlyingType != null)
				return ParseObject(obj, defaultValue, underlyingType, isUnsafe);

			return ParseObject(obj, defaultValue, type, isUnsafe);
		}

		[DebuggerStepThrough]
		private static T ParseObject<T>(object obj, T defaultValue, Type type, bool isUnsafe)
		{
			if(type.GetTypeInfo().IsEnum)
			{
				if(Enum.IsDefined(type, obj)) 
					return (T)Enum.Parse(type, obj.ToString(), true);

				return defaultValue;
			}

			try
			{
				return (T)Convert.ChangeType(obj, type, CultureInfo.CurrentCulture);
			}
			catch(Exception e)
			{
				if(isUnsafe)
				{
					throw;
				}

				return defaultValue;
			}
		}

		/// <summary>
		/// Parse obj to date with format, if couldn't return defValue
		/// </summary>
		public static DateTime ParseToDate(object obj, DateTime defValue, string format)
		{
			if(obj == null)
				return defValue;

			DateTime date;
			if(DateTime.TryParse(obj.ToString(), new DateTimeFormatInfo { ShortDatePattern = format }, DateTimeStyles.None, out date))
				return date;

			return defValue;
		}

		public static bool IsNullOrEmpty(object obj)
		{
			if(obj == null)
				return true;
			return string.IsNullOrEmpty(obj.To<string>(null));
		}
	}
}