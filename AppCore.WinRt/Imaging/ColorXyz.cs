namespace Mt.Common.WinRtAppCore.Imaging
{
	public struct ColorXyz
	{
		private readonly double _alpha;
		private readonly double _x;
		private readonly double _y;
		private readonly double _z;

		public double Alpha
		{
			get { return _alpha; }
		}

		public double X
		{
			get { return _x; }
		}

		public double Y
		{
			get { return _y; }
		}

		public double Z
		{
			get { return _z; }
		}

		public override string ToString()
		{
			const string fmt = "{0}({1:0.0##},{2:0.0##},{3:0.0##},{4:0.0##})";
			return string.Format(System.Globalization.CultureInfo.InvariantCulture, fmt, this.GetType().Name, this._alpha, this._x, this._y, this._z);
		}

		private static void check_xyz(double a, double x, double y, double z)
		{
			ColorConversionUtils.CheckRange_0_1(a, typeof(ColorCmyk), "A");
			//TODO: Add range checking for x,y,z
			//ColorUtil.CheckRange_0_1(x, typeof(ColorCMYK), "X");
			//ColorUtil.CheckRange_0_1(y, typeof(ColorCMYK), "Y");
			//ColorUtil.CheckRange_0_1(z, typeof(ColorCMYK), "Z");
		}

		public ColorXyz(double alpha, double x, double y, double z)
		{
			ColorXyz.check_xyz(alpha, x, y, z);
			this._alpha = alpha;
			this._x = x;
			this._y = y;
			this._z = z;
		}

		public ColorXyz(double x, double y, double z)
			: this(1.0, x, y, z)
		{
		}

		public ColorXyz(ColorRgb rgb, RgbWorkingSpace ws)
		{
			double[,] m = ws.RgbtoxyzMatrix;

			// convert to a sRGB form
			double r = (rgb.R > 0.04045) ? System.Math.Pow((rgb.R + 0.055) / (1.055), 2.4) : (rgb.R / 12.92);
			double g = (rgb.G > 0.04045) ? System.Math.Pow((rgb.G + 0.055) / (1.055), 2.4) : (rgb.G / 12.92);
			double b = (rgb.B > 0.04045) ? System.Math.Pow((rgb.B + 0.055) / (1.055), 2.4) : (rgb.B / 12.92);


			double x = (r * m[0, 0] + g * m[0, 1] + b * m[0, 2]);
			double y = (r * m[1, 0] + g * m[1, 1] + b * m[1, 2]);
			double z = (r * m[2, 0] + g * m[2, 1] + b * m[2, 2]);

			x *= 100;
			y *= 100;
			z *= 100;

			this._alpha = rgb.Alpha;
			this._x = x;
			this._y = y;
			this._z = z;
		}

		public ColorXyz(ColorLab lab, RgbWorkingSpace ws)
		{
			ColorXyz i = ws.ReferenceWhite.ColorXyz;

			double delta = 6.0 / 29.0;

			double fy = (lab.L + 16) / 116.0;
			double fx = fy + (lab.A / 500.0);
			double fz = fy - (lab.B / 200.0);

			double x = (fx > delta) ? i.X * (fx * fx * fx) : (fx - 16.0 / 116.0) * 3 * (delta * delta) * i.X;
			double y = (fy > delta) ? i.Y * (fy * fy * fy) : (fy - 16.0 / 116.0) * 3 * (delta * delta) * i.Y;
			double z = (fz > delta) ? i.Z * (fz * fz * fz) : (fz - 16.0 / 116.0) * 3 * (delta * delta) * i.Z;

			this._alpha = lab.Alpha;
			this._x = x;
			this._y = y;
			this._z = z;
		}
	}
}