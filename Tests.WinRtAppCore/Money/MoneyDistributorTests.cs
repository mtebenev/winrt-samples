using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Mt.Common.Tests.WinRtAppCore.Money
{
	[TestClass]
	public class MoneyDistributorTests
	{
		[TestMethod]
		public void UniformDistributionMustBeBetween0And1()
		{
			MoneyDistributor distributor = new MoneyDistributor(1.0M, FractionReceivers.LastToFirst, RoundingPlaces.Two);

			Assert.ThrowsException<ArgumentOutOfRangeException>(() => distributor.Distribute(0));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => distributor.Distribute(1.1M));
		}


		[TestMethod]
		public void DistributeUniformRatioToLastIsCorrect()
		{
			System.Money amountToDistribute = 0.05M;

			// two decimal places
			MoneyDistributor distributor = new MoneyDistributor(amountToDistribute, FractionReceivers.LastToFirst, RoundingPlaces.Two);

			System.Money[] distribution = distributor.Distribute(0.3M);

			Assert.AreEqual(3, distribution.Length);
			Assert.AreEqual(new System.Money(0.01M), distribution[0]);
			Assert.AreEqual(new System.Money(0.02M), distribution[1]);
			Assert.AreEqual(new System.Money(0.02M), distribution[2]);

			// seven decimal places
			distributor = new MoneyDistributor(amountToDistribute,
																				 FractionReceivers.LastToFirst,
																				 RoundingPlaces.Seven);

			distribution = distributor.Distribute(0.3M);

			Assert.AreEqual(3, distribution.Length);
			Assert.AreEqual(new System.Money(0.0166666M), distribution[0]);
			Assert.AreEqual(new System.Money(0.0166667M), distribution[1]);
			Assert.AreEqual(new System.Money(0.0166667M), distribution[2]);
		}
	}
}
