using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Service.FeeCalculationStrategies;

namespace Domain.Service.Models
{
    public class Merchant
    {
        public string Name { get; }

        public HashSet<DateTime> MonthsWithPaymentsMade { get; }

        public FeeCalculationStrategy FeeCalculationStrategy { get; }

        public Merchant(FeeCalculationStrategy feeCalculationStrategy, string name)
        {
            FeeCalculationStrategy = feeCalculationStrategy;
            Name = name;
            MonthsWithPaymentsMade = new HashSet<DateTime>();
        }

        public void AddPaymentDate(DateTime date)
        {
            MonthsWithPaymentsMade.Add(new DateTime(date.Year, date.Month, 1));
        }

        public bool HasMadePayment(DateTime date)
        {
            return MonthsWithPaymentsMade.Contains(new DateTime(date.Year, date.Month, 1));
        }
    }
}
