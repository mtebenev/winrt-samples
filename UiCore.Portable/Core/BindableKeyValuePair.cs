namespace Mt.Common.UiCore.Core
{
	/// <summary>
	/// Workaround for KeyValuePair binding bug in WinRT:
	/// http://stackoverflow.com/questions/13330938/binding-a-dictionary-to-a-winrt-listbox
	/// </summary>
	public class BindableKeyValuePair
	{
		public BindableKeyValuePair(string key, string value)
		{
			Value = value;
			Key = key;
		}

		public string Key { get; private set; }
		public string Value { get; private set; }
	}
}
