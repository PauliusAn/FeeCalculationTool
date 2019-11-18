using System;

namespace Domain.Service.FeeCalculationStrategies
{
    public class FeeWithDiscountCalculation : FeeCalculationStrategy
    {
        public decimal FeeDiscount { get;}

        public FeeWithDiscountCalculation(decimal feeDiscount)
        {
            if (feeDiscount > 1 || feeDiscount < 0)
            {
                throw new ArgumentException("Fee discount value must be between 0 and 1!");
            }
            FeeDiscount = feeDiscount;
        }

        public override decimal CalculateFee(decimal amount)
        {
            return amount * GetFeeRate() * (1 - FeeDiscount);
        }
    }
}
