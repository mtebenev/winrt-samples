using Windows.UI;

namespace Mt.Common.WinRtAppCore.Imaging
{
	/// <summary>
	/// Extension and helper methods for converting color values
	/// between different RGB data types and different color spaces.
	/// Note: c/p from WinRT XAML toolkit
	/// </summary>
	public static class ColorExtensions
	{
		public static ColorHsl ToHsl(this Color color)
		{
			ColorRgb32Bit c32 = new ColorRgb32Bit(color.A, color.R, color.G, color.B);
			ColorRgb cRgb = new ColorRgb(c32);

			ColorHsl result = new ColorHsl(cRgb);
			return result;
		}

		public static Color ToColor(this ColorHsl hslColor)
		{
			ColorRgb cRgb = new ColorRgb(hslColor);
			ColorRgb32Bit c32 = new ColorRgb32Bit(cRgb);

			return Color.FromArgb(c32.Alpha, c32.R, c32.G, c32.B);
		}

		/// <summary>
		/// Adds luminance using HSL conversion
		/// </summary>
		public static Color AddLuminance(this Color color, double value)
		{
			ColorHsl hslColor = color.ToHsl();
			return hslColor.Add(0, 0, value).ToColor();
		}

		/// <summary>
		/// Adds hue using HSL conversion
		/// </summary>
		public static Color AddHue(this Color color, double value)
		{
			ColorHsl hslColor = color.ToHsl();
			return hslColor.Add(value, 0, 0).ToColor();
		}

		/// <summary>
		/// Adds saturation using HSL conversion
		/// </summary>
		public static Color AddSaturation(this Color color, double value)
		{
			ColorHsl hslColor = color.ToHsl();
			return hslColor.Add(0, value, 0).ToColor();
		}

	}
}
