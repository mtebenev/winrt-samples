using Windows.UI;

namespace Mt.Common.WinRtAppCore.Imaging
{
	/// <summary>
	/// Calculates contrast colors
	/// </summary>
	public interface IContrastColorCalculator
	{
		/// <summary>
		/// Returns another color contrast to given one
		/// </summary>
		Color Calculate(Color color);
	}
}
