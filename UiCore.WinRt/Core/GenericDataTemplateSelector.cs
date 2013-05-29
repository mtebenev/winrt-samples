using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace Mt.Common.UiCore.Core
{
	[ContentProperty(Name = "Template")]
	public class TypeDataTemplate
	{
		public string Type { get; set; }
		public DataTemplate Template { get; set; }
	}

	/// <summary>
	/// Simple generic data template selector. Selects a template by type name
	/// Note: see also http://ovpwp.wordpress.com/2009/01/22/generic-datatemplateselector/ for property value-based template selection
	/// </summary>
	[ContentProperty(Name = "Templates")]
	public class GenericDataTemplateSelector : DataTemplateSelector
	{
		public List<TypeDataTemplate> Templates { get; set; }
		public DataTemplate DefaultTemplate { get; set; }

		public GenericDataTemplateSelector()
		{
			Templates = new List<TypeDataTemplate>();
		}

		protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
		{
			DataTemplate result = DefaultTemplate;

			if(item != null)
			{
				TypeDataTemplate tpl = Templates.SingleOrDefault(t => t.Type == item.GetType().Name);

				if(tpl != null)
					result = tpl.Template;
			}

			return result;
		}
	}
}
