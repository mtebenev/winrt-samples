using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Mt.Common.AppCore.Utils
{
	public static class EnumerableUtils
	{
		/// <summary>
		/// Performs an action on each enumerable member
		/// </summary>
		public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
		{
			if (enumeration == null)
				throw new ArgumentNullException("enumeration");

			if (action == null)
				throw new ArgumentNullException("action");

			foreach (T item in enumeration)
				action(item);
		}

		/// <summary>
		/// Helper for creating observable collection from item list of the same type
		/// </summary>
		public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumeration)
		{
			ObservableCollection<T> result = new ObservableCollection<T>();
			foreach (T item in enumeration)
				result.Add(item);

			return result;
		}

		/// <summary>
		/// Use to avoid 'value cannot be null' exception
		/// </summary>
		public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> collection)
		{
			return collection ?? Enumerable.Empty<T>();
		}

	}
}