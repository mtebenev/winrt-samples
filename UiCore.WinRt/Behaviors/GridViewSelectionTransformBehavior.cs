using System.Collections.Generic;
using Mt.Common.UiCore.Platform;
using Mt.Common.WinRtUiCore.Platform;
using Windows.UI.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Mt.Common.UiCore.Behaviors
{
	/// <summary>
	/// The attached behavior changes row/column span for selected item in GridView
	/// Note: the attached GridView control must be configured with VariableSizedWrapGrid panel
	/// </summary>
	public class GridViewSelectionTransformBehavior : Behavior<GridView>
	{
		protected override void OnAttached()
		{
			base.OnAttached();
			AssociatedObject.SelectionChanged += HandleGridViewSelectionChanged;
		}

		/// <summary>
		/// Number of rows for selected item
		/// </summary>
		public int SelectedRowSpan { get; set; }

		/// <summary>
		/// Number of columns for selected item
		/// </summary>
		public int SelectedColSpan { get; set; }

		/// <summary>
		/// Name of root elemenet in data template. Set when needed to put the selected item in a VSM state
		/// </summary>
		public string TemplateRootElementName { get; set; }

		/// <summary>
		/// Name of selected state in ExVSM
		/// </summary>
		public string SelectedStateName { get; set; }

		/// <summary>
		/// Name of state to put items after deselection
		/// </summary>
		public string NormalStateName { get; set; }

		private void HandleGridViewSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
		{
			GridView gridView = AssociatedObject;

			// Update items added to selection
			ProcessItemsCollection(selectionChangedEventArgs.AddedItems, gridView, this.SelectedStateName, this.SelectedRowSpan, this.SelectedColSpan);

			// Update items removed from selection
			ProcessItemsCollection(selectionChangedEventArgs.RemovedItems, gridView, this.NormalStateName, 1, 1);
		}

		/// <summary>
		/// Processes group of items (actually selected or deselected), updates row/col spans and put the items into specific states
		/// </summary>
		private void ProcessItemsCollection(IList<object> items, GridView gridView, string newStateName, int rowSpan, int colSpan)
		{
			VariableSizedWrapGrid vswGrid = null;
			foreach(object item in items)
			{
				GridViewItem gridViewItem = gridView.ItemContainerGenerator.ContainerFromItem(item) as GridViewItem;
				if(gridViewItem != null)
				{
					VariableSizedWrapGrid.SetRowSpan(gridViewItem, rowSpan);
					VariableSizedWrapGrid.SetColumnSpan(gridViewItem, colSpan);
					
					// Update visual state
					if(newStateName != null && this.TemplateRootElementName != null)
					{
						DependencyObject descendant = VisualTreeEnumerator.FindDescendantByName(gridViewItem, this.TemplateRootElementName, false);

						if(descendant != null)
							ExtendedVisualStateManager.GoToElementState(descendant as Grid, newStateName, true);
					}

					if(vswGrid == null)
						vswGrid = VisualTreeHelper.GetParent(gridViewItem) as VariableSizedWrapGrid;
				}
			}

			// Update panel if some of items were updated
			if(vswGrid != null)
				vswGrid.InvalidateMeasure();
		}
	}
}
