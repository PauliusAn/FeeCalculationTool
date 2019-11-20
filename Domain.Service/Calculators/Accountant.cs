using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Service.Calculators.Decorators;
using Domain.Service.Models;
using Persistence.Read_Models;
using Persistence.TransactionRepository;

namespace Domain.Service.Calculators
{
    public class Accountant
    {
        public HashSet<BaseCalculator> RegisteredCalculators { get; }

        public Accountant()
        {
            RegisteredCalculators = new HashSet<BaseCalculator>();
        }

        public decimal CalculateFee(Transaction transaction)
        {
            if (RegisteredCalculators.All(x => x.Merchant.Name != transaction.MerchantName))
            {
                RegisteredCalculators.Add(new MonthlyFeeCalculator(new FeeCalculator())
                {
                    Merchant = new Merchant(transaction.MerchantName)
                });
            }

            var feeCalculator = RegisteredCalculators.FirstOrDefault(x => x.Merchant.Name == transaction.MerchantName);

            return feeCalculator.CalculateFee(transaction);
        }

        public IEnumerable<Transaction> CalculateAllFees(TransactionRepository transactionRepository)
        {
            Transaction currentTransaction;
            while ((currentTransaction = transactionRepository.GetNextTransaction()) != null)
            {
                if (currentTransaction.MerchantName == string.Empty && currentTransaction.TransferAmount == 0)
                {
                    Console.WriteLine();
                    continue;
                }

                currentTransaction.Fee = CalculateFee(currentTransaction);
                yield return currentTransaction;
            }
        }
    }
}
