namespace Domain.Service.FeeCalculationStrategies
{
    public class BasicFeeCalculation : FeeCalculationStrategy
    {
        public override decimal CalculateFee(decimal amount)
        {
            return amount * GetFeeRate();
        }
    }
}
