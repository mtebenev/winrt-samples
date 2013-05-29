using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Mt.Common.Tests.WinRtAppCore.Money
{
	[TestClass]
	public class MoneyTests
	{
		[TestMethod]
		public void MoneyHasValueEquality()
		{
			System.Money money1 = new System.Money(101.5M);
			System.Money money2 = new System.Money(101.5M);

			Assert.AreEqual(money1, money2);
			Assert.AreNotSame(money1, money2);
		}

		[TestMethod]
		public void MoneyImplicitlyConvertsFromPrimitiveNumbers()
		{
			System.Money money;

			Byte byteValue = 50;
			money = byteValue;
			Assert.AreEqual(new System.Money(50), money);

			SByte sByteValue = 75;
			money = sByteValue;
			Assert.AreEqual(new System.Money(75), money);

			Int16 int16Value = 100;
			money = int16Value;
			Assert.AreEqual(new System.Money(100), money);

			Int32 int32Value = 200;
			money = int32Value;
			Assert.AreEqual(new System.Money(200), money);

			Int64 int64Value = 300;
			money = int64Value;
			Assert.AreEqual(new System.Money(300), money);

			UInt16 uInt16Value = 400;
			money = uInt16Value;
			Assert.AreEqual(new System.Money(400), money);

			UInt32 uInt32Value = 500;
			money = uInt32Value;
			Assert.AreEqual(new System.Money(500), money);

			UInt64 uInt64Value = 600;
			money = uInt64Value;
			Assert.AreEqual(new System.Money(600), money);

			Single singleValue = 700;
			money = singleValue;
			Assert.AreEqual(new System.Money(700), money);

			Double doubleValue = 800;
			money = doubleValue;
			Assert.AreEqual(new System.Money(800), money);

			Decimal decimalValue = 900;
			money = decimalValue;
			Assert.AreEqual(new System.Money(900), money);
		}

		[TestMethod]
		public void MoneyWholeAmountAdditionIsCorrect()
		{
			// whole number
			System.Money money1 = 101M;
			System.Money money2 = 99M;

			Assert.AreEqual(new System.Money(200), money1 + money2);
		}

		[TestMethod]
		public void MoneyFractionalAmountAdditionIsCorrect()
		{
			// fractions
			System.Money money1 = 100.00M;
			System.Money money2 = 0.01M;

			Assert.AreEqual(new System.Money(100.01M), money1 + money2);
		}

		[TestMethod]
		public void MoneyFractionalAmountWithOverflowAdditionIsCorrect()
		{
			// overflow
			System.Money money1 = 100.999M;
			System.Money money2 = 0.9M;

			Assert.AreEqual(new System.Money(101.899M), money1 + money2);
		}

		[TestMethod]
		public void MoneyNegativeAmountAdditionIsCorrect()
		{
			// negative
			System.Money money1 = 100.999M;
			System.Money money2 = -0.9M;

			Assert.AreEqual(new System.Money(100.099M), money1 + money2);
		}

		[TestMethod]
		public void MoneyNegativeAmountWithOverflowAdditionIsCorrect()
		{
			// negative overflow
			System.Money money1 = -100.999M;
			System.Money money2 = -0.9M;

			Assert.AreEqual(new System.Money(-101.899M), money1 + money2);
		}

		[TestMethod]
		public void MoneyWholeAmountSubtractionIsCorrect()
		{
			// whole number
			System.Money money1 = 101M;
			System.Money money2 = 99M;

			Assert.AreEqual(new System.Money(2), money1 - money2);
		}

		[TestMethod]
		public void MoneyFractionalAmountSubtractionIsCorrect()
		{
			// fractions
			System.Money money1 = 100.00M;
			System.Money money2 = 0.01M;

			Assert.AreEqual(new System.Money(99.99M), money1 - money2);
		}

		[TestMethod]
		public void MoneyFractionalAmountWithOverflowSubtractionIsCorrect()
		{
			// overflow
			System.Money money1 = 100.5M;
			System.Money money2 = 0.9M;

			Assert.AreEqual(new System.Money(99.6M), money1 - money2);
		}

		[TestMethod]
		public void MoneyNegativeAmountSubtractionIsCorrect()
		{
			// negative
			System.Money money1 = 100.999M;
			System.Money money2 = -0.9M;

			Assert.AreEqual(new System.Money(101.899M), money1 - money2);
		}

		[TestMethod]
		public void MoneyNegativeAmountWithOverflowSubtractionIsCorrect()
		{
			// negative overflow
			System.Money money1 = -100.999M;
			System.Money money2 = -0.9M;

			Assert.AreEqual(new System.Money(-100.099M), money1 - money2);
		}

		[TestMethod]
		public void MoneyScalarWholeMultiplicationIsCorrect()
		{
			System.Money money = 100.125;

			Assert.AreEqual(new System.Money(500.625M), money * 5);
		}

		[TestMethod]
		public void MoneyScalarFractionalMultiplicationIsCorrect()
		{
			System.Money money = 100.125;

			Assert.AreEqual(new System.Money(50.0625M), money * 0.5M);
		}

		[TestMethod]
		public void MoneyScalarNegativeWholeMultiplicationIsCorrect()
		{
			System.Money money = -100.125;

			Assert.AreEqual(new System.Money(-500.625M), money * 5);
		}

		[TestMethod]
		public void MoneyScalarNegativeFractionalMultiplicationIsCorrect()
		{
			System.Money money = -100.125;

			Assert.AreEqual(new System.Money(-50.0625M), money * 0.5M);
		}

		[TestMethod]
		public void MoneyScalarWholeDivisionIsCorrect()
		{
			System.Money money = 100.125;

			Assert.AreEqual(new System.Money(50.0625M), money / 2);
		}

		[TestMethod]
		public void MoneyScalarFractionalDivisionIsCorrect()
		{
			System.Money money = 100.125;

			Assert.AreEqual(new System.Money(200.25M), money / 0.5M);
		}

		[TestMethod]
		public void MoneyScalarNegativeWholeDivisionIsCorrect()
		{
			System.Money money = -100.125;

			Assert.AreEqual(new System.Money(-50.0625M), money / 2);
		}

		[TestMethod]
		public void MoneyScalarNegativeFractionalDivisionIsCorrect()
		{
			System.Money money = -100.125;

			Assert.AreEqual(new System.Money(-200.25M), money / 0.5M);
		}

		[TestMethod]
		public void MoneyEqualOperatorIsCorrect()
		{
			System.Money money1 = 100.125;
			System.Money money2 = 100.125;

			Assert.IsTrue(money1 == money2);

			money2 = 101.125;
			Assert.IsFalse(money1 == money2);

			money2 = 100.25;
			Assert.IsFalse(money1 == money2);
		}

		[TestMethod]
		public void MoneyNotEqualOperatorIsCorrect()
		{
			System.Money money1 = 100.125;
			System.Money money2 = 100.125;

			Assert.IsFalse(money1 != money2);

			money2 = 101.125;
			Assert.IsTrue(money1 != money2);

			money2 = 100.25;
			Assert.IsTrue(money1 != money2);
		}

		[TestMethod]
		public void MoneyGreaterThanEqualOperatorIsCorrect()
		{
			System.Money money1 = 100.125;
			System.Money money2 = 100.125;

			Assert.IsTrue(money1 >= money2);

			money2 = 101.125;
			Assert.IsTrue(money2 >= money1);
			Assert.IsFalse(money1 >= money2);

			money2 = 100.25;
			Assert.IsTrue(money2 >= money1);
			Assert.IsFalse(money1 >= money2);
		}

		[TestMethod]
		public void MoneyLessThanEqualOperatorIsCorrect()
		{
			System.Money money1 = 100.125;
			System.Money money2 = 100.125;

			Assert.IsTrue(money1 <= money2);

			money2 = 101.125;
			Assert.IsFalse(money2 <= money1);
			Assert.IsTrue(money1 <= money2);

			money2 = 100.25;
			Assert.IsFalse(money2 <= money1);
			Assert.IsTrue(money1 <= money2);
		}

		[TestMethod]
		public void MoneyGreaterThanOperatorIsCorrect()
		{
			System.Money money1 = 100.125;
			System.Money money2 = 100.125;

			Assert.IsFalse(money1 > money2);

			money2 = 101.125;
			Assert.IsTrue(money2 > money1);
			Assert.IsFalse(money1 > money2);

			money2 = 100.25;
			Assert.IsTrue(money2 > money1);
			Assert.IsFalse(money1 > money2);
		}

		[TestMethod]
		public void MoneyLessThanOperatorIsCorrect()
		{
			System.Money money1 = 100.125;
			System.Money money2 = 100.125;

			Assert.IsFalse(money1 < money2);

			money2 = 101.125;
			Assert.IsFalse(money2 < money1);
			Assert.IsTrue(money1 < money2);

			money2 = 100.25;
			Assert.IsFalse(money2 < money1);
			Assert.IsTrue(money1 < money2);
		}

		[TestMethod]
		public void MoneyPrintsCorrectly()
		{
/*
			var previousCulture = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			var money = new System.Money(100.125M, Currency.Usd);
			var formattedMoney = money.ToString();
			Assert.AreEqual("$100.13", formattedMoney);
			Thread.CurrentThread.CurrentCulture = previousCulture;
*/
		}

		[TestMethod]
		public void MoneyOperationsInvolvingDifferentCurrencyAllFail()
		{
			System.Money money1 = new System.Money(101.5M, Currency.Aud);
			System.Money money2 = new System.Money(98.5M, Currency.Cad);
			System.Money m;
			Boolean b;

			Assert.ThrowsException<InvalidOperationException>(() => { m = money1 + money2; });
			Assert.ThrowsException<InvalidOperationException>(() => { m = money1 - money2; });
			Assert.ThrowsException<InvalidOperationException>(() => { b = money1 == money2; });
			Assert.ThrowsException<InvalidOperationException>(() => { b = money1 != money2; });
			Assert.ThrowsException<InvalidOperationException>(() => { b = money1 > money2; });
			Assert.ThrowsException<InvalidOperationException>(() => { b = money1 < money2; });
			Assert.ThrowsException<InvalidOperationException>(() => { b = money1 >= money2; });
			Assert.ThrowsException<InvalidOperationException>(() => { b = money1 <= money2; });
		}

		[TestMethod]
		public void MoneyTryParseIsCorrect()
		{
			string usd = "$123.45";
			string gbp = "£123.45";
			string cad = "CAD123.45";

			System.Money actual;

			bool result = System.Money.TryParse(usd, out actual);
			Assert.IsTrue(result);
			Assert.AreEqual(new System.Money(123.45M, Currency.Usd), actual);

			result = System.Money.TryParse(gbp, out actual);
			Assert.IsTrue(result);
			Assert.AreEqual(new System.Money(123.45M, Currency.Gbp), actual);

			result = System.Money.TryParse(cad, out actual);
			Assert.IsTrue(result);
			Assert.AreEqual(new System.Money(123.45M, Currency.Cad), actual);
		}
	}
}
