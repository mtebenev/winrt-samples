namespace Mt.Common.WinRtAppCore.Imaging
{
	/// <summary>
	/// c/p from viziblr
	/// </summary>
	public class Illuminant
	{
		public string Name { get; private set; }
		public ColorXyz ColorXyz { get; private set; }
		public int Degree { get; private set; }

		public Illuminant(string name, int degree, ColorXyz xyz)
		{
			this.Name = name;
			this.Degree = degree;
			this.ColorXyz = xyz;
		}

	}
}