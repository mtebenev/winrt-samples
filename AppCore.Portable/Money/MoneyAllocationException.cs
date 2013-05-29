namespace System
{
	public class MoneyAllocationException : Exception
	{
		private readonly Money _amountToDistribute;
		private readonly Decimal[] _distribution;
		private readonly Money _distributionTotal;

		public MoneyAllocationException(Money amountToDistribute,
		                                Money distributionTotal,
		                                Decimal[] distribution)
		{
			_amountToDistribute = amountToDistribute;
			_distribution = distribution;
			_distributionTotal = distributionTotal;
		}

		public MoneyAllocationException(Money amountToDistribute,
		                                Money distributionTotal,
		                                Decimal[] distribution,
		                                String message)
			: base(message)
		{
			_amountToDistribute = amountToDistribute;
			_distribution = distribution;
			_distributionTotal = distributionTotal;
		}

		public MoneyAllocationException(Money amountToDistribute,
		                                Money distributionTotal,
		                                Decimal[] distribution,
		                                String message,
		                                Exception inner)
			: base(message, inner)
		{
			_amountToDistribute = amountToDistribute;
			_distribution = distribution;
			_distributionTotal = distributionTotal;
		}

		public Decimal[] Distribution
		{
			get { return _distribution; }
		}

		public Money DistributionTotal
		{
			get { return _distributionTotal; }
		}

		public Money AmountToDistribute
		{
			get { return _amountToDistribute; }
		}
	}
}