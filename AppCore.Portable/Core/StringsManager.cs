using System;

namespace Mt.Common.AppCore.Core
{
	/// <summary>
	/// Use to retrieve localized strings in application. 
	/// Essentially this class wraps platform-specific code for managing localized strings
	/// </summary>
	public class StringsManager
	{
		private static readonly StringsManager _instance;
		private IStringResourceLoader _stringResourceLoader;

		static StringsManager()
		{
			_instance = new StringsManager();
		}

		public static void Initialize(IStringResourceLoader stringResourceLoader)
		{
			_instance._stringResourceLoader = stringResourceLoader;
		}

		public static StringsManager Instance
		{
			get
			{
				// Call Initialize() first in order to use this class
				if(_instance._stringResourceLoader == null)
					throw new InvalidOperationException("Strings manager is not initialized.");

				return _instance;
			}
		}

		/// <summary>
		/// Loads and returns localized string with specified resource name
		/// </summary>
		public string LoadString(string resourceName)
		{
			string result = _stringResourceLoader.LoadString(resourceName);
			return result;
		}

		/// <summary>
		/// Loads localized string template and format that
		/// </summary>
		public string FormatString(string resourceName, params object[] args)
		{
			string template = LoadString(resourceName);
			string result = String.Format(template, args);

			return result;
		}
	}
}
