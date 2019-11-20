using System;
using Persistence.Read_Models;

namespace Domain.Service.Calculators.Decorators
{
    public class FeeDiscountCalculator : CalculatorDecorator
    {
        private readonly decimal _discount;

        public FeeDiscountCalculator(BaseCalculator calculator, decimal discount) : base(calculator)
        {
            if (discount < 0 || discount > 1)
            {
                throw new ArgumentException("Discount cannot be lower than 0 or higher than 1!");
            }

            _discount = discount;
        }

        public override decimal CalculateFee(Transaction transaction)
        {
            return base.CalculateFee(transaction) * (1 -_discount);
        }
    }
}
