namespace Mt.Common.WinRtAppCore.Imaging
{
	/// <summary>
	/// c/p from viziblr
	/// </summary>
	public struct ColorRgb
	{
		private readonly double _alpha;
		private readonly double _r;
		private readonly double _g;
		private readonly double _b;

		public double Alpha
		{
			get { return _alpha; }
		}

		public double R
		{
			get { return _r; }
		}

		public double G
		{
			get { return _g; }
		}

		public double B
		{
			get { return _b; }
		}

		public static void CheckRgbInRange(double a, double r, double g, double b)
		{
			ColorConversionUtils.CheckRange_0_1(a, typeof(ColorRgb), "A");
			ColorConversionUtils.CheckRange_0_1(r, typeof(ColorRgb), "R");
			ColorConversionUtils.CheckRange_0_1(g, typeof(ColorRgb), "G");
			ColorConversionUtils.CheckRange_0_1(b, typeof(ColorRgb), "B");
		}

		public bool IsChromatic()
		{
			return !((this.R == this.G) && (this.G == this.B));
		}

		public static void CheckRgbInRange(double r, double g, double b)
		{
			CheckRgbInRange(1.0, r, g, b);
		}

		public override string ToString()
		{
			return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}({1:0.##},{2:0.0##},{3:0.0##},{4:0.0##})",
			                     this.GetType().Name, _alpha, _r, _g, _b);
		}

		public ColorRgb(double r, double g, double b)
		{
			CheckRgbInRange(r, g, b);
			this._alpha = 1.0;
			this._r = r;
			this._g = g;
			this._b = b;
		}

		public ColorRgb(double alpha, double r, double g, double b)
		{
			CheckRgbInRange(alpha, r, g, b);
			this._alpha = alpha;
			this._r = r;
			this._g = g;
			this._b = b;
		}


		public ColorRgb(int n)
		{
			ColorRgb rgb = FromColor(new ColorRgb32Bit(n));
			this._alpha = rgb.Alpha;
			this._r = rgb.R;
			this._g = rgb.G;
			this._b = rgb.B;
		}

		public ColorRgb(uint n)
		{
			ColorRgb rgb = FromColor(new ColorRgb32Bit(n));
			this._alpha = rgb.Alpha;
			this._r = rgb._r;
			this._g = rgb._g;
			this._b = rgb._b;
		}

		public static explicit operator ColorRgb32Bit(ColorRgb color)
		{
			byte a = (byte) (color._alpha*255);
			byte r = (byte) (color._r*255);
			byte g = (byte) (color._g*255);
			byte b = (byte) (color._b*255);
			return new ColorRgb32Bit(a, r, g, b);
		}


		public static explicit operator int(ColorRgb color)
		{
			ColorRgb32Bit c = new ColorRgb32Bit(color);
			return c.ToInt();
		}

		public ColorRgb(ColorRgb32Bit color)
		{
			ColorRgb temp = FromColor(color);
			this._alpha = temp._alpha;
			this._r = temp._r;
			this._g = temp._g;
			this._b = temp._b;
		}

		public ColorRgb(ColorHsv hsv)
		{
			double XR = double.NaN;
			double XG = double.NaN;
			double XB = double.NaN;

			if(double.IsNaN(hsv.H) || hsv.S == 0.0)
			{
				// Make it some kind of gray
				this._alpha = hsv.Alpha;
				this._r = hsv.V;
				this._g = hsv.V;
				this._b = hsv.V;
				return;
			}

			double H = hsv.H;
			if(hsv.H > 1.0)
			{
				throw new System.ArgumentOutOfRangeException("H");
			}
			else if(hsv.H == 1.0)
			{
				H = 0.0;
			}

			double step = 1.0/6.0;
			double vh = H/step;

			int i = (int) System.Math.Floor(vh);

			double f = vh - i;
			double p = hsv.V*(1.0 - hsv.S);
			double q = hsv.V*(1.0 - (hsv.S*f));
			double t = hsv.V*(1.0 - (hsv.S*(1.0 - f)));

			switch(i)
			{
				case 0:
					{
						XR = hsv.V;
						XG = t;
						XB = p;
						break;
					}
				case 1:
					{
						XR = q;
						XG = hsv.V;
						XB = p;
						break;
					}
				case 2:
					{
						XR = p;
						XG = hsv.V;
						XB = t;
						break;
					}
				case 3:
					{
						XR = p;
						XG = q;
						XB = hsv.V;
						break;
					}
				case 4:
					{
						XR = t;
						XG = p;
						XB = hsv.V;
						break;
					}
				case 5:
					{
						XR = hsv.V;
						XG = p;
						XB = q;
						break;
					}
				default:
					{
						// not possible - if we get here it is an internal error
						throw new System.ArgumentException();
					}
			}

			this._alpha = hsv.Alpha;
			this._r = XR;
			this._g = XG;
			this._b = XB;
		}

		private static double hue_2_rgb(double m1, double m2, double h)
		{
			h = ColorConversionUtils.NormalizeHue(h);

			if((6.0*h) < 1.0)
			{
				return (m1 + (m2 - m1)*6.0*h);
			}

			if((2.0*h) < 1.0)
			{
				return m2;
			}

			if((3.0*h) < 2.0)
			{
				return m1 + (m2 - m1)*((2.0/3.0) - h)*6.0;
			}

			return m1;
		}


		public ColorRgb(ColorHsl hsl)
		{
			if(double.IsNaN(hsl.H) || hsl.S == 0) //HSL values = From 0 to 1
			{
				this._alpha = hsl.Alpha;
				this._r = hsl.L; //RGB results = From 0 to 255
				this._g = hsl.L;
				this._b = hsl.L;
				return;
			}

			double m2 = (hsl.L < 0.5) ? hsl.L*(1.0 + hsl.S) : (hsl.L + hsl.S) - (hsl.S*hsl.L);
			double m1 = (2.0*hsl.L) - m2;
			const double onethird = (1.0/3.0);

			this._alpha = hsl.Alpha;
			this._r = 1.0*hue_2_rgb(m1, m2, hsl.H + onethird);
			this._g = 1.0*hue_2_rgb(m1, m2, hsl.H);
			this._b = 1.0*hue_2_rgb(m1, m2, hsl.H - onethird);
		}

		public ColorRgb(ColorCmyk cmyk)
		{
			double CMY_C = cmyk.C*(1 - cmyk.K) + cmyk.K;
			double CMY_M = cmyk.M*(1 - cmyk.K) + cmyk.K;
			double CMY_Y = cmyk.Y*(1 - cmyk.K) + cmyk.K;

			this._alpha = cmyk.Alpha;
			this._r = 1 - CMY_C;
			this._g = 1 - CMY_M;
			this._b = 1 - CMY_Y;
		}

		public ColorRgb(ColorXyz xyz, RgbWorkingSpace ws)
		{
			var m = ws.XyztoRgbMatrix;

			double x = xyz.X/100;
			double y = xyz.Y/100;
			double z = xyz.Z/100;

			double lin_r = (x*m[0, 0]) + (y*m[0, 1]) + (z*m[0, 2]); // red
			double lin_g = (x*m[1, 0]) + (y*m[1, 1]) + (z*m[1, 2]); // green
			double lin_b = (x*m[2, 0]) + (y*m[2, 1]) + (z*m[2, 2]); // blue

			double r = (lin_r <= 0.0031308) ? 12.92*lin_r : (1.055)*System.Math.Pow(lin_r, (1.0/2.4)) - 0.055;
			double g = (lin_g <= 0.0031308) ? 12.92*lin_g : (1.055)*System.Math.Pow(lin_g, (1.0/2.4)) - 0.055;
			double b = (lin_b <= 0.0031308) ? 12.92*lin_b : (1.055)*System.Math.Pow(lin_b, (1.0/2.4)) - 0.055;

			r = ClampToRange_0_1(r);
			g = ClampToRange_0_1(g);
			b = ClampToRange_0_1(b);

			this._alpha = xyz.Alpha;
			this._r = r;
			this._g = g;
			this._b = b;
		}

		private static double ClampToRange_0_1(double component)
		{
			if(component < 0.0)
			{
				component = 0.0;
			}
			else if(component > 1.0)
			{
				component = 1.0;
			}
			return component;
		}

		private static ColorRgb FromColor(ColorRgb32Bit color)
		{
			ColorRgb c = new ColorRgb(color.Alpha/255.0, color.R/255.0, color.G/255.0, color.B/255.0);
			return c;
		}
	}
}