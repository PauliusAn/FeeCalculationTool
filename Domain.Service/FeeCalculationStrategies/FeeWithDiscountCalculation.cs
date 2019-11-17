namespace Domain.Service.FeeCalculationStrategies
{
    public class FeeWithDiscountCalculation : FeeCalculationStrategy
    {
        public decimal FeeDiscount { get;}

        public FeeWithDiscountCalculation(decimal feeDiscount)
        {
            FeeDiscount = feeDiscount;
        }

        public override decimal CalculateFee(decimal amount)
        {
            return amount * GetFeeRate() * (1 - FeeDiscount);
        }
    }
}
