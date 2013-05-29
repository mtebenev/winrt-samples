using System;
using System.Text;

namespace Mt.Common.AppCore.Utils
{
	/// <summary>
	/// Various string helpers
	/// </summary>
	public static class StringUtils
	{
		public static string GenerateRandomString(Random random, int maxLength)
		{
			int stringLength = random.Next(0, maxLength);

			StringBuilder builder = new StringBuilder();
			for(int i = 0; i < stringLength; i++)
			{
				char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
				builder.Append(ch);
			}

			return builder.ToString();
		}
	}
}
