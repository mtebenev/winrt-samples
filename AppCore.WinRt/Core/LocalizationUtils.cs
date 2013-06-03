using Windows.ApplicationModel.Resources;

namespace Mt.Common.AppCore.Core
{
	/// <summary>
	/// WinRT-specific resource loader
	/// </summary>
	public class StringResourceLoader : IStringResourceLoader
	{
		public string LoadString(string resourceName)
		{
			ResourceLoader rl = new ResourceLoader();
			string result = rl.GetString(resourceName);

			return result;
		}
	}
}
