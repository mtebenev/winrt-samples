using Windows.UI;

namespace Mt.Common.WinRtAppCore.Imaging
{
	/// <summary>
	/// Simple black/white implementation
	/// </summary>
	public class ContrastColorCalculatorBw : IContrastColorCalculator
	{
		public Color Calculate(Color color)
		{
			Color result = Colors.Black;
			
			ColorHsl hslColor = color.ToHsl();

			if(hslColor.L < 0.5)
				result = Colors.White;

			return result;
		}
	}
}