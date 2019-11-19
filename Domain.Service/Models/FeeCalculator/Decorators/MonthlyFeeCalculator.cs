using System;
using Persistence.Read_Models;

namespace Domain.Service.Models.FeeCalculator.Decorators
{
    class MonthlyFeeCalculator : CalculatorDecorator
    {
        private DateTime _lastMonthWithFeePaid;
        private readonly decimal _monthlyFee = 29;

        public MonthlyFeeCalculator(BaseCalculator calculator) : base(calculator)
        {
        }

        public override decimal CalculateFee(Transaction transaction)
        {
            var transferDate = transaction.Date.AddDays(-transaction.Date.Day);

            if (transferDate <= _lastMonthWithFeePaid)
            {
                return base.CalculateFee(transaction);
            }

            _lastMonthWithFeePaid = transferDate;
            return base.CalculateFee(transaction) + _monthlyFee;
        }
    }
}
