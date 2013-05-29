using System;
using System.Globalization;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Mt.CommercePlatform.Tests.WinRtAppCore.Money
{
	[TestClass]
	public class CurrencyTests
	{
		[TestMethod]
		public void CurrencyFromCurrentCultureEqualsCurrentCultureCurrency()
		{
			Currency currency1 = new Currency(new RegionInfo(CultureInfo.CurrentCulture.Name).ISOCurrencySymbol);
			Currency currency2 = Currency.FromCurrentCulture();

			Assert.AreEqual(currency1.Name, currency2.Name);
			Assert.AreEqual(currency1.Symbol, currency2.Symbol);
			Assert.AreEqual(currency1.Iso3LetterCode, currency2.Iso3LetterCode);
			Assert.AreEqual(currency1.IsoNumericCode, currency2.IsoNumericCode);
		}

		[TestMethod]
		public void CurrencyFromSpecificCultureInfoIsCorrect()
		{
			CultureInfo cultureInfo = new CultureInfo("en-ZA");
			Currency currency = Currency.FromCultureInfo(cultureInfo);
			Assert.AreEqual(710, currency.IsoNumericCode);
		}

		[TestMethod]
		public void CurrencyFromSpecificIsoCodeIsCorrect()
		{
			Currency currency = Currency.FromIso3LetterCode("EUR");

			Assert.AreEqual(978, currency.IsoNumericCode);
		}

		[TestMethod]
		public void CurrencyHasValueEquality()
		{
			Currency currency1 = new Currency("USD");
			Currency currency2 = new Currency("USD");
			object boxedCurrency2 = currency2;

			Assert.IsTrue(currency1.Equals(currency2));
			Assert.IsTrue(currency1.Equals(boxedCurrency2));
		}
	}
}
