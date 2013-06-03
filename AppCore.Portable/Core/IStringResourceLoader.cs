namespace Mt.Common.AppCore.Core
{
	/// <summary>
	/// Performs loading string resources. Implementations are platform-specific
	/// </summary>
	public interface IStringResourceLoader
	{
		/// <summary>
		/// The method loads string resource with specified name
		/// </summary>
		string LoadString(string resourceName);
	}
}