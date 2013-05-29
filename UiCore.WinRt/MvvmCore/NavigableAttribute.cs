using System;

namespace Mt.Common.UiCore.MvvmCore
{
	/// <summary>
	/// Specifies the page URI associated with an alias (target view name).
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public class NavigableAttribute : Attribute
	{
		public const string DefaultTarget = "default";

		/// <summary>
		/// Initializes a new instance of the <see cref="NavigableAttribute"/> class.
		/// </summary>
		/// <param name="pageType">Page type</param>
		public NavigableAttribute(Type pageType)
		{
			this.Target = DefaultTarget;
			this.PageType = pageType;
		}

		/// <summary>
		/// Gets or sets the target view name used for navigation.
		/// </summary>
		public string Target { get; set; }

		/// <summary>
		/// Gets or sets type of GUI page
		/// </summary>
		public Type PageType { get; set; }
	}
}
