namespace Mt.Common.WinRtAppCore.Imaging
{
	public struct ColorHsl
	{
		private readonly double _alpha;
		private readonly double _h;
		private readonly double _s;
		private readonly double _l;

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

		public double L
		{
			get { return _l; }
		}

		public override string ToString()
		{
			return string.Format(System.Globalization.CultureInfo.InvariantCulture,
			                     "{0}({1:0.0##},{2:0.0##},{3:0.0##},{3:0.0##})", this.GetType().Name, this._alpha, this._h,
			                     this._s, this._l);
		}

		public static void CheckHslInRange(double _h, double _s, double _l)
		{
			CheckHslInRange(1.0, _h, _s, _l);
		}

		public static void CheckHslInRange(double _a, double _h, double _s, double _l)
		{
			ColorConversionUtils.CheckRange_0_1(_a, typeof(ColorHsl), "A");
			ColorConversionUtils.CheckRange_0_1_NAN(_h, typeof(ColorHsl), "H");
			ColorConversionUtils.CheckRange_0_1_NAN(_s, typeof(ColorHsl), "S");
			ColorConversionUtils.CheckRange_0_1(_l, typeof(ColorHsl), "L");
			ColorConversionUtils.CheckCompatibleHS(_h, _s);
		}

		public bool IsChromatic()
		{
			return (!double.IsNaN(this.H) && !double.IsNaN(this.S));
		}

		public ColorHsl(double alpha, double h, double s, double l)
		{
			CheckHslInRange(alpha, h, s, l);
			this._alpha = alpha;
			this._h = h;
			this._s = s;
			this._l = l;
		}

		public ColorHsl(double h, double s, double l)
		{
			CheckHslInRange(h, s, l);
			this._alpha = 1.0;
			this._h = h;
			this._s = s;
			this._l = l;
		}

		public ColorHsl(ColorHsl hsl)
		{
			this._alpha = hsl._alpha;
			this._h = hsl._h;
			this._s = hsl._s;
			this._l = hsl._l;
		}

		public ColorHsl(ColorRgb rgb)
		{
			double maxc = System.Math.Max(rgb.R, System.Math.Max(rgb.G, rgb.B));
			double minc = System.Math.Min(rgb.R, System.Math.Min(rgb.G, rgb.B));
			double delta = maxc - minc;

			double l = (maxc + minc)/2.0;
			double h = double.NaN;
			double s = double.NaN;

			// Handle case for r,g,b all have the same value
			if(maxc == minc)
			{
				// Black, White, or some shade of Gray -> No Chroma
				this._alpha = rgb.Alpha;
				this._h = double.NaN;
				this._s = double.NaN;
				this._l = l;
				return;
			}

			// At this stage, we know R,G,B are not all set to the same value - i.e. there Chroma
			if(l < 0.5)
				s = delta/(maxc + minc);
			else
				s = delta/(2.0 - maxc - minc);

			double rc = (((maxc - rgb.R)/6.0) + (delta/2.0))/delta;
			double gc = (((maxc - rgb.G)/6.0) + (delta/2.0))/delta;
			double bc = (((maxc - rgb.B)/6.0) + (delta/2.0))/delta;

			h = 0.0;

			if(rgb.R == maxc)
				h = bc - gc;
			else if(rgb.G == maxc)
				h = (1.0/3.0) + rc - bc;
			else if(rgb.B == maxc)
				h = (2.0/3.0) + gc - rc;

			h = ColorConversionUtils.NormalizeHue(h);

			this._alpha = rgb.Alpha;
			this._h = h;
			this._s = s;
			this._l = l;
		}

		public ColorHsl Add(double h, double s, double l)
		{
			double newH = ColorConversionUtils.NormalizeHue(this._h + h);
			double newS = ColorConversionUtils.NormalizeSaturation(this._s + s);
			double newL = ColorConversionUtils.NormalizeLightness(this._l + l);
			
			return new ColorHsl(newH, newS, newL);
		}
	}
}