using Persistence.Read_Models;

namespace Domain.Service.Models.FeeCalculator.Decorators
{
    public class CalculatorDecorator : BaseCalculator
    {
        protected BaseCalculator _calculator;

        public CalculatorDecorator(BaseCalculator calculator)
        {
            _calculator = calculator;
        }

        public override decimal CalculateFee(Transaction transaction)
        {
            return _calculator.CalculateFee(transaction);
        }
    }
}
