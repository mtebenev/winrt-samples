using Windows.ApplicationModel.Resources;

namespace Mt.Common.UiCore.Core
{
	/// <summary>
	/// Various helpers for working with localized strings
	/// </summary>
	public static class LocalizationUtils
	{
		/// <summary>
		/// Loads localized string from resources
		/// </summary>
		public static string LoadLocalizedString(string resourceName)
		{
			ResourceLoader rl = new ResourceLoader();
			string result = rl.GetString(resourceName);

			return result;
		}
	}
}
