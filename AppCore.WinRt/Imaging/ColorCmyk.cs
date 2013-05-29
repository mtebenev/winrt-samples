namespace Mt.Common.WinRtAppCore.Imaging
{
	public struct ColorCmyk
	{
		private readonly double _alpha;
		private readonly double _c;
		private readonly double _m;
		private readonly double _y;
		private readonly double _k;

		public double Alpha
		{
			get { return _alpha; }
		}

		public double C
		{
			get { return _c; }
		}

		public double M
		{
			get { return _m; }
		}

		public double Y
		{
			get { return _y; }
		}

		public double K
		{
			get { return _k; }
		}

		public override string ToString()
		{
			const string fmt = "{0}({1:0.0##},{2:0.0##},{3:0.0##},{4:0.0##},{5:0.0##})";
			return string.Format(System.Globalization.CultureInfo.InvariantCulture, fmt, this.GetType().Name, this._alpha, this._c, this._m, this._y, this._k);
		}

		private static void check_cmyk(double a, double c, double m, double y, double k)
		{
			ColorConversionUtils.CheckRange_0_1(a, typeof(ColorCmyk), "A");
			ColorConversionUtils.CheckRange_0_1(c, typeof(ColorCmyk), "C");
			ColorConversionUtils.CheckRange_0_1(m, typeof(ColorCmyk), "M");
			ColorConversionUtils.CheckRange_0_1(y, typeof(ColorCmyk), "Y");
			ColorConversionUtils.CheckRange_0_1(k, typeof(ColorCmyk), "K");
		}

		private static void check_cmyk(double c, double m, double y, double k)
		{
			check_cmyk(1.0, c, m, y, k);
		}

		public ColorCmyk(double alpha, double c, double m, double y, double k)
		{
			ColorCmyk.check_cmyk(alpha, c, m, y, k);
			this._alpha = alpha;
			this._c = c;
			this._m = m;
			this._y = y;
			this._k = k;
		}

		public ColorCmyk(double c, double m, double y, double k)
		{
			ColorCmyk.check_cmyk(c, m, y, k);
			this._alpha = 1.0;
			this._c = c;
			this._m = m;
			this._y = y;
			this._k = k;
		}

		public ColorCmyk(ColorRgb rgb)
		{
			if(rgb.R == 0.0 && rgb.G == 0.0 && rgb.B == 0.0)
			{
				_alpha = rgb.Alpha;
				_c = 0.0;
				_m = 0.0;
				_y = 0.0;
				_k = 1.0;
				return;
			}

			double CMY_Cyan = 1 - rgb.R;
			double CMY_Magenta = 1 - rgb.G;
			double CMY_Yellow = 1 - rgb.B;

			double var_K = 1.0;

			if(CMY_Cyan < var_K)
			{
				var_K = CMY_Cyan;
			}

			if(CMY_Magenta < var_K)
			{
				var_K = CMY_Magenta;
			}

			if(CMY_Yellow < var_K)
			{
				var_K = CMY_Yellow;
			}

			_alpha = rgb.Alpha;
			_c = (CMY_Cyan - var_K) / (1 - var_K);
			_m = (CMY_Magenta - var_K) / (1 - var_K);
			_y = (CMY_Yellow - var_K) / (1 - var_K);
			_k = var_K;
		}
	}
}