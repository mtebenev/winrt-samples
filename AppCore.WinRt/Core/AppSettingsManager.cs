using Windows.Storage;

namespace Mt.Common.WinRtAppCore.Core
{
	/// <summary>
	/// Performs application settings management
	/// </summary>
	public class AppSettingsManager
	{
		public void TestWrite()
		{
			ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

			localSettings.Values["Test"] = "Testix";

			ApplicationDataCompositeValue compositeValue = new ApplicationDataCompositeValue();
			compositeValue["Val1"] = "teststring_1";
			compositeValue["Val2"] = 121;

			localSettings.Values["CompVal"] = compositeValue;

			

			//StorageFolder localFolder = ApplicationData.Current.LocalFolder;

		}

		public void TestRead()
		{
			ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

			object o = localSettings.Values["Test"];

			object o1 = localSettings.Values["CompVal"];

			int a = 5;

			//StorageFolder localFolder = ApplicationData.Current.LocalFolder;

		}
	}
}
