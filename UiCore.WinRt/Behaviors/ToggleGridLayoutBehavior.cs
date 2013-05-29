using Windows.UI.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Mt.Common.UiCore.Behaviors
{
	/// <summary>
	/// Switches columns widths and rows heights between two states
	/// </summary>
	public class ToggleGridLayoutBehavior : Behavior<Grid>
	{
		/// <summary>
		/// Indicates that grid is in toggled state or not
		/// </summary>
		public static readonly DependencyProperty IsToggledProperty =
			DependencyProperty.Register("IsToggled", typeof(bool), typeof(ToggleGridLayoutBehavior),
			new PropertyMetadata(false, HandleIsToggledPropertyChanged));

		public bool IsToggled
		{
			get { return (bool)GetValue(IsToggledProperty); }
			set { SetValue(IsToggledProperty, value); }
		}

		/// <summary>
		/// Use to set column or row measure in toggled state
		/// </summary>
		public static readonly DependencyProperty ToggledMeasureProperty =
			DependencyProperty.RegisterAttached("ToggledMeasure", typeof(GridLength), typeof(ToggleGridLayoutBehavior),
			new PropertyMetadata(DependencyProperty.UnsetValue));

		public static void SetToggledMeasure(DependencyObject element, GridLength value)
		{
			element.SetValue(ToggledMeasureProperty, value);
		}
		public static GridLength GetToggledMeasure(DependencyObject element)
		{
			return (GridLength)element.GetValue(ToggledMeasureProperty);
		}

		/// <summary>
		/// Stores original measure of a column or row (width or height)
		/// </summary>
		internal static readonly DependencyProperty OriginalMeasureProperty =
			DependencyProperty.RegisterAttached("OriginalMeasure", typeof(GridLength), typeof(ToggleGridLayoutBehavior),
			new PropertyMetadata(DependencyProperty.UnsetValue));

		protected override void OnAttached()
		{
			base.OnAttached();

			// Store original column widths as attached properties
			foreach(ColumnDefinition definition in AssociatedObject.ColumnDefinitions)
			{
				GridLength measure = definition.Width;
				definition.SetValue(OriginalMeasureProperty, measure);
			}
		}

		private static void HandleIsToggledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ToggleGridLayoutBehavior behavior = (ToggleGridLayoutBehavior) d;
			bool isToggled = (bool) e.NewValue;

			behavior.ToggleGridMeasures(isToggled);
		}

		private void ToggleGridMeasures(bool isToggled)
		{
			// Change column measures
			foreach(ColumnDefinition definition in AssociatedObject.ColumnDefinitions)
			{
				object toggledMeasure = definition.ReadLocalValue(ToggledMeasureProperty);
				if(toggledMeasure != DependencyProperty.UnsetValue) // Toggled value has been set on the object
				{
					object newMeasure = isToggled ? toggledMeasure : definition.ReadLocalValue(OriginalMeasureProperty);
					definition.SetValue(ColumnDefinition.WidthProperty, newMeasure);
				}
			}
		}
	}
}
