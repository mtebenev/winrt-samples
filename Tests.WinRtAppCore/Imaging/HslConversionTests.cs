using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Mt.Common.WinRtAppCore.Imaging;
using Windows.UI;

namespace Mt.CommercePlatform.Tests.WinRtAppCore.Imaging
{
	// ReSharper disable InconsistentNaming
	[TestClass]
	public class HslConversionTests
	{
		/// <summary>
		/// RGB --> HSL --> RGB must produce the same result
		/// </summary>
		[TestMethod]
		public void Conversion_Must_Not_Change_Original_Color()
		{
			string[] colors = new string[] { "#FFFF4040", "#FF78D2C8" };

			foreach(string colorString in colors)
			{
				Color color = ColorUtils.ColorFromString(colorString);
				ColorHsl hslColor = color.ToHsl();
				Color color2 = hslColor.ToColor();

				Assert.AreEqual(color, color2);
			}
		}
	}
}
