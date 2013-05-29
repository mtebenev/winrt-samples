using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Mt.Common.WinRtAppCore.Imaging
{
	/// <summary>
	/// Static helper class related to color.
	/// </summary>
	public static class ColorUtils
	{
		/// <summary>
		/// The ARGB values corresponding to the <see cref="NamedColors"/>.
		/// </summary>
		public static readonly IList<string> NamedColorsRgb = new List<string>
		{
			"#F0F8FF", "#FAEBD7", "#00FFFF", "#7FFFD4", "#F0FFFF", "#F5F5DC", "#FFE4C4", "#000000", "#FFEBCD", "#0000FF", "#8A2BE2", "#A52A2A", "#DEB887",
			"#5F9EA0", "#7FFF00", "#D2691E", "#FF7F50", "#6495ED", "#FFF8DC", "#DC143C", "#00FFFF", "#00008B", "#008B8B", "#B8860B", "#A9A9A9", "#006400", "#BDB76B", "#8B008B",
			"#556B2F", "#FF8C00", "#9932CC", "#8B0000", "#E9967A", "#8FBC8F", "#483D8B", "#2F4F4F", "#00CED1", "#9400D3", "#FF1493", "#00BFFF", "#696969", "#1E90FF", "#D19275",
			"#B22222", "#FFFAF0", "#228B22", "#FF00FF", "#DCDCDC", "#F8F8FF", "#FFD700", "#DAA520", "#808080", "#008000", "#ADFF2F", "#F0FFF0", "#FF69B4", "#CD5C5C", "#4B0082",
			"#FFFFF0", "#F0E68C", "#E6E6FA", "#FFF0F5", "#7CFC00", "#FFFACD", "#ADD8E6", "#F08080", "#E0FFFF", "#FAFAD2", "#D3D3D3", "#90EE90", "#FFB6C1", "#FFA07A", "#20B2AA",
			"#87CEFA", "#8470FF", "#778899", "#B0C4DE", "#FFFFE0", "#00FF00", "#32CD32", "#FAF0E6", "#FF00FF", "#800000", "#66CDAA", "#0000CD", "#BA55D3", "#9370D8", "#3CB371",
			"#7B68EE", "#00FA9A", "#48D1CC", "#C71585", "#191970", "#F5FFFA", "#FFE4E1", "#FFE4B5", "#FFDEAD", "#000080", "#FDF5E6", "#808000", "#6B8E23", "#FFA500", "#FF4500",
			"#DA70D6", "#EEE8AA", "#98FB98", "#AFEEEE", "#D87093", "#FFEFD5", "#FFDAB9", "#CD853F", "#FFC0CB", "#DDA0DD", "#B0E0E6", "#800080", "#FF0000", "#BC8F8F", "#4169E1",
			"#8B4513", "#FA8072", "#F4A460", "#2E8B57", "#FFF5EE", "#A0522D", "#C0C0C0", "#87CEEB", "#6A5ACD", "#708090", "#FFFAFA", "#00FF7F", "#4682B4", "#D2B48C", "#008080",
			"#D8BFD8", "#FF6347", "#40E0D0", "#EE82EE", "#D02090", "#F5DEB3", "#FFFFFF", "#F5F5F5", "#FFFF00", "#9ACD32"
		};

		/// <summary>
		/// Know color names taken over from .Net.
		/// </summary>
		public static readonly IList<string> NamedColors = new List<string>
		{
		    "AliceBlue", "AntiqueWhite", "Aqua", "Aquamarine", "Azure", "Beige", "Bisque", "Black", "BlanchedAlmond", "Blue", "BlueViolet", "Brown",
		    "BurlyWood", "CadetBlue", "Chartreuse", "Chocolate", "Coral", "CornflowerBlue", "Cornsilk", "Crimson", "Cyan", "DarkBlue", "DarkCyan", "DarkGoldenRod", "DarkGray",
		    "DarkGreen", "DarkKhaki", "DarkMagenta", "DarkOliveGreen", "Darkorange", "DarkOrchid", "DarkRed", "DarkSalmon", "DarkSeaGreen", "DarkSlateBlue", "DarkSlateGray",
		    "DarkTurquoise", "DarkViolet", "DeepPink", "DeepSkyBlue", "DimGray", "DodgerBlue", "Feldspar", "FireBrick", "FloralWhite", "ForestGreen", "Fuchsia", "Gainsboro",
		    "GhostWhite", "Gold", "GoldenRod", "Gray", "Green", "GreenYellow", "HoneyDew", "HotPink", "IndianRed", "Indigo", "Ivory", "Khaki", "Lavender", "LavenderBlush",
		    "LawnGreen", "LemonChiffon", "LightBlue", "LightCoral", "LightCyan", "LightGoldenRodYellow", "LightGrey", "LightGreen", "LightPink", "LightSalmon", "LightSeaGreen",
		    "LightSkyBlue", "LightSlateBlue", "LightSlateGray", "LightSteelBlue", "LightYellow", "Lime", "LimeGreen", "Linen", "Magenta", "Maroon", "MediumAquaMarine",
		    "MediumBlue", "MediumOrchid", "MediumPurple", "MediumSeaGreen", "MediumSlateBlue", "MediumSpringGreen", "MediumTurquoise", "MediumVioletRed", "MidnightBlue",
		    "MintCream", "MistyRose", "Moccasin", "NavajoWhite", "Navy", "OldLace", "Olive", "OliveDrab", "Orange", "OrangeRed", "Orchid", "PaleGoldenRod", "PaleGreen",
		    "PaleTurquoise", "PaleVioletRed", "PapayaWhip", "PeachPuff", "Peru", "Pink", "Plum", "PowderBlue", "Purple", "Red", "RosyBrown", "RoyalBlue", "SaddleBrown",
		    "Salmon", "SandyBrown", "SeaGreen", "SeaShell", "Sienna", "Silver", "SkyBlue", "SlateBlue", "SlateGray", "Snow", "SpringGreen", "SteelBlue", "Tan", "Teal", "Thistle",
		    "Tomato", "Turquoise", "Violet", "VioletRed", "Wheat", "White", "WhiteSmoke", "Yellow", "YellowGreen"
		};

		private static readonly Random Rand = new Random();

		/// <summary>
		/// Gets a random shade of blue.
		/// </summary>
		/// <value>The random blue color.</value>
		public static Color RandomBlues
		{
			get
			{
				double red = ((Rand.NextDouble() * 20D) + 150D) / 255D;
				double green = ((Rand.NextDouble() * 150D) + 100D) / 255D;
				double blue = ((Rand.NextDouble() * 50D) + 150D) / 255D;

				return Color.FromArgb(255, (byte)red, (byte)green, (byte)blue);
			}
		}

		/// <summary>
		/// Gets the random brush where the color is picked from the known colors.
		/// </summary>
		/// <value>The random brush.</value>
		public static Brush RandomBrush
		{
			get
			{
				return ParseToBrush(NamedColorsRgb[Rand.Next(0, NamedColorsRgb.Count)]);
			}
		}

		/// <summary>
		/// Returns brush from ARGB values.
		/// </summary>
		/// <param name="alfa">The alfa.</param>
		/// <param name="red">The red.</param>
		/// <param name="green">The green.</param>
		/// <param name="blue">The blue.</param>
		public static Brush BrushFromArgb(byte alfa, byte red, byte green, byte blue)
		{
			return new SolidColorBrush(new Color { A = alfa, R = red, G = green, B = blue });
		}

		/// <summary>
		/// Converts the given byte array to a color in the format #AARRGGBB.
		/// </summary>
		/// <param name="value">The bytes.</param>
		public static string ByteArrayToHexString(byte[] value)
		{
			const string HexAlphabet = "0123456789ABCDEF";

			StringBuilder result = new StringBuilder();

			foreach(byte v in value)
			{
				result.Append(HexAlphabet[v >> 4]);
				result.Append(HexAlphabet[v & 0xF]);
			}

			return result.ToString();
		}

		/// <summary>
		/// Converts the #AARRGGBB string color to a byte array.
		/// </summary>
		/// <param name="color">The hex string value of the color.</param>
		public static byte[] HexStringToByteArray(string color)
		{
			const string HexValue = "\x0\x1\x2\x3\x4\x5\x6\x7\x8\x9|||||||\xA\xB\xC\xD\xE\xF";

			int byteLength = color.Length / 2;
			byte[] bytes = new byte[byteLength];

			for(int x = 0, i = 0; i < color.Length; i += 2, x += 1)
			{
				bytes[x] = (byte)(HexValue[char.ToUpper(color[i + 0]) - '0'] << 4);
				bytes[x] |= (byte)HexValue[char.ToUpper(color[i + 1]) - '0'];
			}

			return bytes;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="color">The color.</param>
		public static string HexStringFromSolidColor(Color color)
		{
			return ByteArrayToHexString(new[] { color.A, color.R, color.G, color.B });
		}

		/// <summary>
		/// Converts the specified color to a format #AARRGGBB. Use the <see cref="ColorToString"/> to convert a string back to a color.
		/// </summary>
		/// <param name="color">
		/// The value to convert.
		/// </param>
		/// <returns>
		/// A string representing the specified value.
		/// </returns>
		public static string ColorToString(Color color)
		{
			return string.Format("#{0}{1}{2}{3}", color.A.ToString("X2"), color.R.ToString("X2"), color.G.ToString("X2"), color.B.ToString("X2"));
		}

		/// <summary>
		/// Parses to brush.
		/// </summary>
		/// <param name="hexValue">The hex string.</param>
		/// <returns></returns>
		public static SolidColorBrush ParseToBrush(string hexValue)
		{
			return new SolidColorBrush(ColorFromString(hexValue));
		}

		/// <summary>
		/// Colors from string.
		/// </summary>
		/// <param name="hexValue">The hex string.</param>
		public static Color ColorFromString(string hexValue)
		{
			if(hexValue.StartsWith("#", StringComparison.Ordinal))
			{
				hexValue = hexValue.Substring(1);
				//// means the alpha is missing
				if(hexValue.Length == 6)
					hexValue = hexValue.Insert(0, "FF");

				byte a = HexStringToByteArray(hexValue.Substring(0, 2))[0];
				byte r = HexStringToByteArray(hexValue.Substring(2, 2))[0];
				byte g = HexStringToByteArray(hexValue.Substring(4, 2))[0];
				byte b = HexStringToByteArray(hexValue.Substring(6, 2))[0];
				return Color.FromArgb(a, r, g, b);
			}

			return Colors.White;
		}

		/// <summary>
		/// Adds the two given colors.
		/// </summary>
		/// <param name="color1">The first color.</param>
		/// <param name="color2">The second color.</param>
		/// <returns></returns>
		public static Color Sum(Color color1, Color color2)
		{
			Color color = new Color
			{
				A = 0xff,
				R = Convert.ToByte(color1.R + color2.R),
				G = Convert.ToByte(color1.G + color2.G),
				B = Convert.ToByte(color1.B + color2.B)
			};
			return color;
		}

		/// <summary>
		/// Returns the unsigned integer value of the color.
		/// </summary>
		/// <param name="color">The color.</param>                
		public static int ToValue(Color color)
		{
			int a = color.A;
			int r = color.R;
			int g = color.G;
			int b = color.B;
			return ((a << 0x18 | r << 0x10) | g << 8) | b;
		}

		/// <summary>
		/// Parses the specified value and converts it to a color.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>        
		public static Color Parse(int value)
		{
			Color color = new Color { B = (byte)(value & 0xff) };
			value = value >> 8;
			color.G = (byte)(value & 0xff);
			value = value >> 8;
			color.R = (byte)(value & 0xff);
			value = value >> 8;
			color.A = (byte)(value & 0xff);
			if(color.A == 0)
				color.A = 0xff;

			return color;
		}

		/// <summary>
		/// Creates a color from an angle.
		/// </summary>
		/// <param name="value">The angle in degrees.</param>
		public static Color FromAngle(double value)
		{
			byte r = 0;
			byte g = 0;
			byte b = 0;
			Color color = new Color();
			if(value < 0.0)
			{
				value = (value % 360.0) + 360.0;
			}
			if(value > 360.0)
			{
				value = value % 360.0;
			}
			if((value >= 0.0) && (value <= 60.0))
			{
				r = 0xff;
				g = (byte)Math.Round((value / 60.0) * 255.0);
				b = 0;
			}
			else if((value > 60.0) && (value <= 120.0))
			{
				r = (byte)Math.Round(((120.0 - value) / 60.0) * 255.0);
				g = 0xff;
				b = 0;
			}
			else if((value > 120.0) && (value <= 180.0))
			{
				r = 0;
				g = 0xff;
				b = (byte)Math.Round(((value - 120.0) / 60.0) * 255.0);
			}
			else if((value > 180.0) && (value <= 240.0))
			{
				r = 0;
				g = (byte)Math.Round(((240.0 - value) / 60.0) * 255.0);
				b = 0xff;
			}
			else if((value > 240.0) && (value <= 300.0))
			{
				r = (byte)Math.Round(((value - 240.0) / 60.0) * 255.0);
				g = 0;
				b = 0xff;
			}
			else if((value > 300.0) && (value <= 360.0))
			{
				r = 0xff;
				g = 0;
				b = (byte)Math.Round(((360.0 - value) / 60.0) * 255.0);
			}
			color.R = r;
			color.G = g;
			color.B = b;
			color.A = 0xff;
			return color;
		}

		/// <summary>
		/// Multiplies/scales the specified color.
		/// </summary>
		/// <param name="color">The color.</param>
		/// <param name="factor">The factor.</param>
		public static Color Multiply(this Color color, double factor)
		{
			return new Color
			{
				A = 0xff,
				R = Convert.ToByte(Math.Floor(Convert.ToDouble(color.R * factor))),
				G = Convert.ToByte(Math.Floor(Convert.ToDouble(color.G * factor))),
				B = Convert.ToByte(Math.Floor(Convert.ToDouble(color.B * factor)))
			};
		}

		/// <summary>
		/// Parses the specified color string (e.g. '#FF4B4578').
		/// </summary>
		/// <param name="value">The string representation of the color.</param>
		public static Color Parse(string value)
		{
			if(string.IsNullOrEmpty(value)) throw new ArgumentNullException("value");
			string str;
			Color color = new Color();
			if(value.Substring(0, 1) == "#")
			{
				str = value.Substring(1);
			}
			else if(value.Substring(0, 2) == "0x")
			{
				str = value.Substring(2);
			}
			else
			{
				str = value;
			}
			for(int i = str.Length; i < 6; i++)
			{
				str = str + "0";
			}
			if(str.Length == 6)
			{
				color.A = 0xff;
				color.R = byte.Parse(str.Substring(0, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
				color.G = byte.Parse(str.Substring(2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
				color.B = byte.Parse(str.Substring(4, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
				return color;
			}
			if(str.Length == 8)
			{
				color.A = byte.Parse(str.Substring(0, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
				color.R = byte.Parse(str.Substring(2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
				color.G = byte.Parse(str.Substring(4, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
				color.B = byte.Parse(str.Substring(6, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			}
			return color;
		}
	}
}
