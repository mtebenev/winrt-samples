using System.Collections.Generic;

namespace Mt.Common.WinRtAppCore.Imaging
{
	public class RgbWorkingSpaces
	{
		// Working Space Values from: http://www.brucelindbloom.com/index.html?Eqn_RGB_XYZ_Matrix.html

		public RgbWorkingSpace SRGB_D65_Degree2 { get; private set; }
		public RgbWorkingSpace Adobe_D65_Degree2 { get; private set; }
		public List<RgbWorkingSpace> Spaces;

		public RgbWorkingSpaces()
		{
			this.SRGB_D65_Degree2 =
					new RgbWorkingSpace("sRGB D65 2", Illuminants.RefWhite_D65_2degree,
															new double[3, 3]
                                    {
                                        {3.2404542, -1.5371385, -0.4985314},
                                        {-0.9692660, 1.8760108, 0.0415560},
                                        {0.0556434, -0.2040259, 1.0572252}
                                    },
															new double[3, 3]
                                    {
                                        {0.4124564, 0.3575761, 0.1804375},
                                        {0.2126729, 0.7151522, 0.0721750},
                                        {0.0193339, 0.1191920, 0.9503041}
                                    });

			this.Adobe_D65_Degree2 =
					new RgbWorkingSpace("Adobe RGB (1998) D65 2", Illuminants.RefWhite_D65_2degree,
															new double[3, 3]
                                    {
                                        {2.0413690, -0.5649464, -0.3446944},
                                        {-0.9692660, 1.8760108, 0.0415560},
                                        {0.0134474, -0.1183897, 1.0154096}
                                    },
															new double[3, 3]
                                    {
                                        {0.5767309, 0.1855540, 0.1881852},
                                        {0.2973769, 0.6273491, 0.0752741},
                                        {0.0270343, 0.0706872, 0.9911085}
                                    });

			this.Spaces = new List<RgbWorkingSpace>();
			this.Spaces.Add(SRGB_D65_Degree2);
			this.Spaces.Add(Adobe_D65_Degree2);
		}
	}
}