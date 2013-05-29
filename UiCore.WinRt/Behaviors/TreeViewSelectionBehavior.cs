using Mt.Common.UiCore.Controls;
using Mt.Common.UiCore.Controls.TreeView;
using Windows.UI.Interactivity;
using Windows.UI.Xaml;

namespace Mt.Common.UiCore.Behaviors
{
	/// <summary>
	/// Enables binding for TreeView selected item
	/// Based on http://joshzx.blogspot.ru/2010/10/silverlight-toolkits-treeview.html
	/// Note: functionality for selecting item in the control is cut because it's ugly (primitive iteration over whole tree)
	/// This behavior essentially sets selected item in view model. For selecting tree view item IsSelected and IsExpanded properties
	/// in bound item models should be used
	/// </summary>
	public class TreeViewSelectionBehavior : Behavior<TreeView>
	{
		/// <summary> 
		/// Bound selected item
		/// </summary> 
		public static readonly DependencyProperty SelectedItemProperty =
			DependencyProperty.RegisterAttached("SelectedItem",
			typeof(object), typeof(TreeViewSelectionBehavior),
			new PropertyMetadata(DependencyProperty.UnsetValue));
		
		public static void SetSelectedItem(DependencyObject o, object propertyValue)
		{
			o.SetValue(SelectedItemProperty, propertyValue);
		}

		public static object GetSelectedItem(DependencyObject o)
		{
			return o.GetValue(SelectedItemProperty);
		}

		protected override void OnAttached()
		{
			base.OnAttached();
			AssociatedObject.SelectedItemChanged += HandleTreeViewSelectedItemChanged;
		}

		protected override void OnDetaching()
		{
			AssociatedObject.SelectedItemChanged -= HandleTreeViewSelectedItemChanged;
			base.OnDetaching();
		}

		private void HandleTreeViewSelectedItemChanged(object d, RoutedPropertyChangedEventArgs<object> e)
		{
			SetSelectedItem(this, e.NewValue);
		}
	}
}
