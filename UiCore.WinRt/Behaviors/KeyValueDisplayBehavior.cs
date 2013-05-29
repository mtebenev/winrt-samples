using System;
using System.Collections.Generic;
using System.Linq;
using Mt.Common.UiCore.Core;
using Windows.UI.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Mt.Common.UiCore.Behaviors
{
	/// <summary>
	/// Fills attached grid with bound Key/value pairs.
	/// Note: grid must define at least two columns and the behavior puts the records in the two first columns
	/// </summary>
	public class KeyValueDisplayGridBehavior : Behavior<Grid>
	{
		/// <summary>
		/// Bound items property
		/// </summary>
		public static readonly DependencyProperty ItemsProperty =
		DependencyProperty.Register("Items", typeof(object), typeof(KeyValueDisplayGridBehavior),
		new PropertyMetadata(DependencyProperty.UnsetValue, HandleItemsChanged));

		public IEnumerable<BindableKeyValuePair> Items
		{
			get { return (IEnumerable<BindableKeyValuePair>)GetValue(ItemsProperty); }
			set { SetValue(ItemsProperty, value); }
		}

		// Styles for key and value textblocks
		public Style KeyTextBlockStyle { get; set; }
		public Style ValueTextBlockStyle { get; set; }

		protected override void OnAttached()
		{
			base.OnAttached();

			if(this.Items != null)
				FillGrid();
		}

		private static void HandleItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			KeyValueDisplayGridBehavior behavior = (KeyValueDisplayGridBehavior)d;

			if(behavior.AssociatedObject != null)
				behavior.FillGrid();
		}

		private void FillGrid()
		{
			Grid grid = this.AssociatedObject;

			if(grid.ColumnDefinitions.Count < 2)
				throw new InvalidOperationException("Define at least 2 columns in the grid");

			// Clear rows
			grid.Children.Clear();
			grid.RowDefinitions.Clear();

			if(this.Items != null)
				AddItemsToGrid(grid);
		}

		private void AddItemsToGrid(Grid grid)
		{
			// Insert row definitions
			int rowCount = Items.Count();
			for(int i = 0; i < rowCount; i++)
			{
				RowDefinition rowDefinition = new RowDefinition();
				grid.RowDefinitions.Add(rowDefinition);
			}

			// Insert rows
			int row = 0;
			foreach(BindableKeyValuePair pair in this.Items)
			{
				// Add key
				TextBlock textBlockKey =
					new TextBlock
						{
							Text = pair.Key,
							Style = this.KeyTextBlockStyle
						};

				textBlockKey.SetValue(Grid.ColumnProperty, 0);
				textBlockKey.SetValue(Grid.RowProperty, row);
				grid.Children.Add(textBlockKey);

				// Add value
				TextBlock textBlockValue =
					new TextBlock
						{
							Text = pair.Value,
							Style = this.ValueTextBlockStyle
						};

				textBlockValue.SetValue(Grid.ColumnProperty, 1);
				textBlockValue.SetValue(Grid.RowProperty, row);
				grid.Children.Add(textBlockValue);

				row++;
			}
		}
	}
}
