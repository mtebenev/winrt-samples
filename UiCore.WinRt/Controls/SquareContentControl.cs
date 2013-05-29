using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Mt.Common.UiCore.Controls
{
	/// <summary>
	/// Renders content in square
	/// </summary>
	public class SquareContentControl : ContentControl
	{
		public SquareContentControl()
		{
			VerticalAlignment = VerticalAlignment.Stretch;
			HorizontalAlignment = HorizontalAlignment.Stretch;
			VerticalContentAlignment = VerticalAlignment.Stretch;
			HorizontalContentAlignment = HorizontalAlignment.Stretch;
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			base.MeasureOverride(availableSize);
			double s =  Math.Min(availableSize.Width, availableSize.Height);

			return new Size(s, s);
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			double sideLength = Math.Max(finalSize.Width, finalSize.Height);
			Size result = base.ArrangeOverride(new Size(sideLength, sideLength));

			return result;
		}
	}
}
