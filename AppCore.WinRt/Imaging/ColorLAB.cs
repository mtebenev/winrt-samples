namespace Mt.Common.WinRtAppCore.Imaging
{
	/// c/p from viziblr
	public struct ColorLab
	{
		private readonly double _a;
		private readonly double _alpha;
		private readonly double _l;
		private readonly double _b;

		public double Alpha
		{
			get { return _alpha; }
		}

		public double A
		{
			get { return _a; }
		}

		public double L
		{
			get { return _l; }
		}

		public double B
		{
			get { return _b; }
		}

		public override string ToString()
		{
			const string fmt = "{0}({1:0.0##},{2:0.0##},{3:0.0##},{4:0.0##})";
			return string.Format(System.Globalization.CultureInfo.InvariantCulture, fmt, this.GetType().Name, this._alpha,
			                     this._l, this._a, this._b);
		}

		private static void check_lab(double alpha, double l, double a, double b)
		{
			ColorConversionUtils.CheckRange_0_1(alpha, typeof(ColorCmyk), "Alpha");
			//TODO: Add range checking for x,y,z
			//ColorUtil.CheckRange_0_1(x, typeof(ColorCMYK), "X");
			//ColorUtil.CheckRange_0_1(y, typeof(ColorCMYK), "Y");
			//ColorUtil.CheckRange_0_1(z, typeof(ColorCMYK), "Z");
		}

		public ColorLab(double alpha, double l, double a, double b)
		{
			ColorLab.check_lab(alpha, l, a, b);
			this._alpha = alpha;
			this._l = l;
			this._a = a;
			this._b = b;
		}

		public ColorLab(double l, double a, double b)
			: this(1.0, l, a, b)
		{
		}

		// Note: working spaces were cut from code base. Restore if needed.
/*
		public ColorLab(ColorXYZ xyz, RGBWorkingSpace ws)
		{
			ColorXYZ i = ws.ReferenceWhite.ColorXYZ;

			double nx = xyz.X/i.X;
			double ny = xyz.Y/i.Y;
			double nz = xyz.Z/i.Z;

			double fx = (nx > 0.008856) ? System.Math.Pow(nx, (1.0/3.0)) : (7.787*nx + 16.0/116.0);
			double fy = (ny > 0.008856) ? System.Math.Pow(ny, (1.0/3.0)) : (7.787*ny + 16.0/116.0);
			double fz = (nz > 0.008856) ? System.Math.Pow(nz, (1.0/3.0)) : (7.787*nz + 16.0/116.0);

			double l = (116.0*fy) - 16;
			double a = 500.0*(fx - fy);
			double b = 200.0*(fy - fz);

			this._alpha = xyz.Alpha;
			this._l = l;
			this._a = a;
			this._b = b;
		}
*/
	}
}