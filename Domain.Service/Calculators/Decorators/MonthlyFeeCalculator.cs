using System;
using Persistence.Read_Models;

namespace Domain.Service.Calculators.Decorators
{
    public class MonthlyFeeCalculator : CalculatorDecorator
    {
        private DateTime _lastMonthWithFeePaid;
        private readonly decimal _monthlyFee = 29;

        public MonthlyFeeCalculator(BaseCalculator calculator) : base(calculator)
        {
        }

        public override decimal CalculateFee(Transaction transaction)
        {
            var transferDate = transaction.Date.AddDays(-transaction.Date.Day + 1);

            if (transferDate <= _lastMonthWithFeePaid)
            {
                return base.CalculateFee(transaction);
            }

            _lastMonthWithFeePaid = transferDate;
            return base.CalculateFee(transaction) + _monthlyFee;
        }

        public decimal GetMonthlyFeeAmount()
        {
            return _monthlyFee;
        }

        public DateTime GetLastPaidMonth()
        {
            return _lastMonthWithFeePaid;
        }
    }
}
