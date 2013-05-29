/*
 * 
 * CREDITS
 * -------
 * The HSV<->RGB Conversion code based on this source code: http://www.cs.rit.edu/~ncs/color/t_convert.html
 * from Eugene Vishnevsky
 * 
 */

namespace Mt.Common.WinRtAppCore.Imaging
{
	public struct ColorHsv
	{
		private readonly double _alpha;
		private readonly double _h;
		private readonly double _s;
		private readonly double _v;

		public double Alpha
		{
			get { return _alpha; }
		}

		public double H
		{
			get { return _h; }
		}

		public double S
		{
			get { return _s; }
		}

		public double V
		{
			get { return _v; }
		}

		public override string ToString()
		{
			return string.Format(System.Globalization.CultureInfo.InvariantCulture,
			                     "{0}({1:0.0##},{2:0.0##},{3:0.0##},{4:0.0##})", this.GetType().Name, this._alpha, this._h,
			                     this._s, this._v);
		}

		public static void CheckHSVInRange(double _h, double _s, double _v)
		{
			CheckHSVInRange(1.0, _h, _s, _v);
		}

		public static void CheckHSVInRange(double _a, double _h, double _s, double _v)
		{
			ColorConversionUtils.CheckRange_0_1(_a, typeof(ColorHsv), "A");
			ColorConversionUtils.CheckRange_0_1_NAN(_h, typeof(ColorHsv), "H");
			ColorConversionUtils.CheckRange_0_1_NAN(_s, typeof(ColorHsv), "S");
			ColorConversionUtils.CheckRange_0_1(_v, typeof(ColorHsv), "V");
			ColorConversionUtils.CheckCompatibleHS(_h, _s);
		}

		public bool IsChromatic()
		{
			return (!double.IsNaN(this.H) && !double.IsNaN(this.S));
		}

		public ColorHsv(double hue, double sat, double val)
		{
			CheckHSVInRange(hue, sat, val);
			this._alpha = 1.0;
			this._h = hue;
			this._s = sat;
			this._v = val;
		}

		public ColorHsv(double alpha, double hue, double sat, double val)
		{
			CheckHSVInRange(alpha, hue, sat, val);
			this._alpha = alpha;
			this._h = hue;
			this._s = sat;
			this._v = val;
		}


		public ColorHsv(ColorHsv hc)
		{
			this._alpha = hc._alpha;
			this._h = hc._h;
			this._s = hc._s;
			this._v = hc._v;
		}

		public ColorHsv(ColorRgb rgb)
		{
			double maxc = System.Math.Max(rgb.R, System.Math.Max(rgb.G, rgb.B));
			double minc = System.Math.Min(rgb.R, System.Math.Min(rgb.G, rgb.B));

			double h = double.NaN;
			double s = double.NaN;
			double v = double.NaN;

			// Handle case for r,g,b all have the same value
			if(maxc == minc)
			{
				// Black, White, or some shade of Gray -> No Chroma
				this._alpha = rgb.Alpha;
				this._h = double.NaN;
				this._s = double.NaN;
				this._v = maxc;
				return;
			}

			// At this stage, we know R,G,B are not all set to the same value - i.e. there is Chromatic data
			double delta = maxc - minc;
			s = delta/maxc;
			v = maxc;

			if(rgb.R == maxc)
			{
				h = 0.0 + ((rgb.G - rgb.B)/delta);
			}
			else if(rgb.G == maxc)
			{
				h = 2.0 + ((rgb.B - rgb.R)/delta);
			}
			else
			{
				h = 4.0 + ((rgb.R - rgb.G)/delta);
			}

			h = ColorConversionUtils.NormalizeHue(h/6.0);

			this._alpha = rgb.Alpha;
			this._h = h;
			this._s = s;
			this._v = v;
		}
	}
}