namespace Mt.Common.WinRtAppCore.Imaging
{
	public class RgbWorkingSpace
	{
		public string Name { get; private set; }
		public Illuminant ReferenceWhite { get; private set; }
		public double[,] XyztoRgbMatrix { get; private set; }
		public double[,] RgbtoxyzMatrix { get; private set; }

		public RgbWorkingSpace(string name, Illuminant refwhite, double[,] xyz_to_rgb, double[,] rgb_to_xyz)
		{
			this.Name = name;
			this.ReferenceWhite = refwhite;
			this.XyztoRgbMatrix = xyz_to_rgb;
			this.RgbtoxyzMatrix = rgb_to_xyz;
		}
	}
}