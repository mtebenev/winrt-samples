namespace System
{

	/// <summary>
	/// Provides fake attributes for compatibility with net35-based xml serialization contracts
	/// </summary>
	public class SerializableAttribute : Attribute
	{
	}
}

namespace System.ComponentModel
{
	public class DesignerCategoryAttribute : Attribute
	{
		public DesignerCategoryAttribute(string s)
		{
		}
	}
}
