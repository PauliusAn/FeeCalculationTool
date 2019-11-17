namespace Domain.Service.FeeCalculationStrategies
{
    public abstract class FeeCalculationStrategy
    {
        private const decimal FeeRate = 0.01m;

        public decimal GetFeeRate()
        {
            return FeeRate;
        }

        public abstract decimal CalculateFee(decimal amount);
    }
}
