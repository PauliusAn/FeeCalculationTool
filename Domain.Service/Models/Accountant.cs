using System.Collections.Generic;
using System.Linq;
using Domain.Service.Models.FeeCalculator;
using Domain.Service.Models.FeeCalculator.Decorators;
using Persistence.Read_Models;

namespace Domain.Service.Models
{
    public class Accountant : BaseCalculator
    {
        public HashSet<BaseCalculator> RegisteredCalculators { get; }

        public Accountant()
        {
            RegisteredCalculators = new HashSet<BaseCalculator>();
        }

        public override decimal CalculateFee(Transaction transaction)
        {
            if (RegisteredCalculators.All(x => x.Merchant.Name != transaction.MerchantName))
            {
                RegisteredCalculators.Add(new MonthlyFeeCalculator(new FeeCalculator.FeeCalculator())
                {
                    Merchant = new Merchant(transaction.MerchantName)
                });
            }

            var feeCalculator = RegisteredCalculators.FirstOrDefault(x => x.Merchant.Name == transaction.MerchantName);

            return feeCalculator.CalculateFee(transaction);
        }
    }
}
