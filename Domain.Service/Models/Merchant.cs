using System;
using Domain.Service.FeeCalculationStrategies;

namespace Domain.Service.Models
{
    public class Merchant
    {
        public string Name { get; }

        public DateTime LastDateMonthlyFeeWasPaid { get; set; }

        public FeeCalculationStrategy FeeCalculationStrategy { get; }

        public Merchant(FeeCalculationStrategy feeCalculationStrategy, string name)
        {
            FeeCalculationStrategy = feeCalculationStrategy;
            Name = name;
        }

        public bool IsMonthlyFeePaid(DateTime date)
        {
            if (LastDateMonthlyFeeWasPaid == DateTime.MinValue)
                return false;
            date = date.AddDays(-date.Day);
            var paidMonth = LastDateMonthlyFeeWasPaid.AddDays(-LastDateMonthlyFeeWasPaid.Day);

            return date <= paidMonth;
        }
    }
}
