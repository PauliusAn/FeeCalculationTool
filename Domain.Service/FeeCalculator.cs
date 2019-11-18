using System.Collections.Generic;
using System.Linq;
using Domain.Service.FeeCalculationStrategies;
using Domain.Service.Models;
using Persistence.Read_Models;

namespace Domain.Service
{
    public class FeeCalculator
    {
        public HashSet<Merchant> RegisteredMerchants { get; }
        private readonly decimal _fixedMonthlyFee = 29;

        public FeeCalculator()
        {
            RegisteredMerchants = new HashSet<Merchant>();
        }

        public decimal CalculateFee(Transaction transaction)
        {
            if (RegisteredMerchants.All(x => x.Name != transaction.MerchantName))
            {
                RegisteredMerchants.Add(new Merchant(new BasicFeeCalculation(), transaction.MerchantName));
            }

            decimal totalFee = 0;
            var merchant = RegisteredMerchants.FirstOrDefault(x => x.Name == transaction.MerchantName);

            if (!merchant.HasMadePayment(transaction.Date))
            {
                totalFee += _fixedMonthlyFee;
                merchant.AddPaymentDate(transaction.Date);
            }

            totalFee += merchant.FeeCalculationStrategy.CalculateFee(transaction.TransferAmount);

            return totalFee;
        }
    }
}
