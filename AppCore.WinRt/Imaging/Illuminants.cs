namespace Mt.Common.WinRtAppCore.Imaging
{
	public static class Illuminants
	{
		// Iluminants from: http://blog.elevenseconds.com/code/colorConversions.txt

		public static readonly Illuminant RefWhite_A_2degree = new Illuminant("A", 2, new ColorXyz(109.850, 100, 35.585));
		public static readonly Illuminant RefWhite_C_2degree = new Illuminant("C", 2, new ColorXyz(98.074, 100, 118.232));
		public static readonly Illuminant RefWhite_D50_2degree = new Illuminant("D50", 2, new ColorXyz(96.422, 100, 82.521));
		public static readonly Illuminant RefWhite_D55_2degree = new Illuminant("D55", 2, new ColorXyz(95.682, 100, 92.149));
		public static readonly Illuminant RefWhite_D65_2degree = new Illuminant("D65", 2, new ColorXyz(95.047, 100, 108.883));
		public static readonly Illuminant RefWhite_D75_2degree = new Illuminant("D75", 2, new ColorXyz(94.972, 100, 122.638));
		public static readonly Illuminant RefWhite_F2_2degree = new Illuminant("F2", 2, new ColorXyz(99.187, 100, 67.395));
		public static readonly Illuminant RefWhite_F7_2degree = new Illuminant("F7", 2, new ColorXyz(95.044, 100, 108.755));
		public static readonly Illuminant RefWhite_F11_2degree = new Illuminant("F11", 2, new ColorXyz(100.966, 100, 64.370));
		public static readonly Illuminant RefWhite_A_10degree = new Illuminant("A", 10, new ColorXyz(111.144, 100, 35.200));
		public static readonly Illuminant RefWhite_C_10degree = new Illuminant("C", 10, new ColorXyz(97.285, 100, 116.145));
		public static readonly Illuminant RefWhite_D50_10degree = new Illuminant("D50", 10, new ColorXyz(96.720, 100, 81.427));
		public static readonly Illuminant RefWhite_D55_10degree = new Illuminant("D55", 10, new ColorXyz(95.799, 100, 90.926));
		public static readonly Illuminant RefWhite_D65_10degree = new Illuminant("D65", 10, new ColorXyz(94.811, 100, 107.304));
		public static readonly Illuminant RefWhite_D75_10degree = new Illuminant("D75", 10, new ColorXyz(94.416, 100, 120.641));
		public static readonly Illuminant RefWhite_F2_10degree = new Illuminant("F2", 10, new ColorXyz(103.280, 100, 69.026));
		public static readonly Illuminant RefWhite_F7_10degree = new Illuminant("F7", 10, new ColorXyz(95.792, 100, 107.687));
		public static readonly Illuminant RefWhite_F11_10degree = new Illuminant("F11", 10, new ColorXyz(103.866, 100, 65.627));
	}
}