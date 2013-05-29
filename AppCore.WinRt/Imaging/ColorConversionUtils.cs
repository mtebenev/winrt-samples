namespace Mt.Common.WinRtAppCore.Imaging
{
	/// <summary>
	/// c/p from viziblr
	/// </summary>
	public static class ColorConversionUtils
	{
		/*
         * The Color conversion is inspired by the colorssys module in the Python standard library
         * Unlike most code that works with HSL and HSV, instead of using 0.0 when there is an unknown Hue
         * this library used double.Nan to represent unvalid Hue values
         * 
         * Other Resources and implementations
         * http://en.wikipedia.org/wiki/HLS_color_space
         * http://en.wikipedia.org/wiki/HSV_color_space
         * http://www.easyrgb.com/
         * http://www.cs.rit.edu/~ncs/color/t_convert.html
         * http://www.tecgraf.puc-rio.br/~mgattass/color/HSLtoRGB.htm
         * http://www.tecgraf.puc-rio.br/~mgattass/color/HSLtoRGB.htm 
         * http://www.tecgraf.puc-rio.br/~mgattass/color/CMYKtoCMY.htm 
         * http://www.tecgraf.puc-rio.br/~mgattass/color/CMYtoRGB.htm
         * http://www.tecgraf.puc-rio.br/~mgattass/color/RGBtoCMY.htm
         * http://www.tecgraf.puc-rio.br/~mgattass/color/CMYtoCMYK.htm
         * http://www.codeproject.com/Articles/19045/Manipulating-colors-in-NET-Part-1
         * http://cookbooks.adobe.com/post_Useful_color_equations__RGB_to_LAB_converter-14227.html
         */

		public static double NormalizeHue(double v)
		{
			const double min = 0.0;
			const double max = 1.0;

			// Note that 1.0, 2.0, 3.0 will all come back as zero

			if((min <= v) && (v < max))
			{
				// the number is already in the range so do nothing
				return v;
			}

			// outherwise perform the equivalent of mod
			return v - (max * System.Math.Floor(v / max));
		}

		public static double NormalizeSaturation(double v)
		{
			if(v < 0.0)
			{
				return 0.0;
			}
			else if(v > 1.0)
			{
				return 1.0;
			}
			return v;
		}

		public static double NormalizeLightness(double v)
		{
			if(v < 0.0)
			{
				return 0.0;
			}
			else if(v > 1.0)
			{
				return 1.0;
			}
			return v;
		}

		internal static void CheckRange_0_1(double v, System.Type t, string name)
		{
			if(!((0.0 <= v) && (v <= 1.0)))
			{
				string msg = string.Format("{0} {1} out of range. Must be [0 to 1.0]", t.Name, name);
				throw new ColorException(msg);
			}
		}

		internal static void CheckRange_0_1_NAN(double v, System.Type t, string name)
		{
			if(!(double.IsNaN(v) || ((0.0 <= v) && (v <= 1.0))))
			{
				string msg = string.Format("{0} {1} out of range. Must be [0 to 1.0] or NaN", t.Name, name);
				throw new ColorException(msg);
			}
		}

		internal static void CheckCompatibleHS(double _h, double _s)
		{
			if(double.IsNaN(_h) && !double.IsNaN(_s))
			{
				throw new ColorException("Achromatic values must have both H and S set to NAN");
			}

			if(double.IsNaN(_s) && !double.IsNaN(_h))
			{
				throw new ColorException("Achromatic values must have both H ans S set to NAN");
			}
		}
	}
}