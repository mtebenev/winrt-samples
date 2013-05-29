using System.Linq;
using System.Reflection;
using Mt.Common.AppCore.Utils;

namespace Mt.Common.UiCore.MvvmCore
{
	public abstract class ViewModelBase : NotificationObject
	{
		/// <summary>
		/// Raise property changed for all public properties of current class
		/// Note: so far this method cannot be used well because WinRT does not provide a way to retrieve only public properties
		/// </summary>
		protected void RaiseAllPublicPropertiesChanged()
		{
			GetType()
				.GetRuntimeProperties()
				.Where(p => p.CanRead) // TODO: need to retrieve only public properties
				.ForEach(propertyInfo => RaisePropertyChanged(propertyInfo.Name));
		}
	}
}
