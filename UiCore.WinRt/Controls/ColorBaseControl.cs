using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Mt.Common.UiCore.Controls
{
	public abstract class ColorBaseControl : Control
	{
		public delegate void ColorChangedHandler(object sender, Color color);
		public event ColorChangedHandler ColorChanged;

		#region dependency properties
		public Color Color
		{
			get { return (Color)GetValue(ColorProperty); }
			set { SetValue(ColorProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ColorProperty =
				DependencyProperty.Register("Color", typeof(Color), typeof(ColorBaseControl), new PropertyMetadata(null, OnColorChanged));

		public SolidColorBrush SolidColorBrush
		{
			get { return (SolidColorBrush)GetValue(SolidColorBrushProperty); }
			private set { SetValue(SolidColorBrushProperty, value); }
		}

		// Using a DependencyProperty as the backing store for SolidColorBrush.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty SolidColorBrushProperty =
				DependencyProperty.Register("SolidColorBrush", typeof(SolidColorBrush), typeof(ColorBaseControl), new PropertyMetadata(new SolidColorBrush()));
		#endregion

		private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ColorBaseControl colorControl = d as ColorBaseControl;

			if(colorControl != null)
			{
				colorControl.UpdateLayoutBasedOnColor();
				colorControl.SolidColorBrush = new SolidColorBrush((Color)e.NewValue);
			}
		}

		protected internal virtual void UpdateLayoutBasedOnColor()
		{
		}

		protected internal void ColorChanging(Color color)
		{
			Color = color;
			SolidColorBrush = new SolidColorBrush(Color);

			if(ColorChanged != null)
				ColorChanged(this, Color);
		}
	}
}
