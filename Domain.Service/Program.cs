using System;
using Domain.Service.Calculators;
using Domain.Service.Calculators.Decorators;
using Domain.Service.Models;
using Persistence.FileSystem;
using Persistence.TransactionRepository;

namespace Domain.Service
{
    class Program
    {
        private const string TransactionsFilePath = "../../../../transactions.txt";

        static void Main()
        {
            EnsureFileExistence();

            var transactionRepository = new TransactionRepository(new FileReader(TransactionsFilePath));
            var feeAccountant = new Accountant();

            RegisterMerchants(feeAccountant);

            foreach (var transaction in feeAccountant.CalculateAllFees(transactionRepository))
            {
                Console.WriteLine(transaction);
            }
        }

        

        private static void RegisterMerchants(Accountant accountant)
        {
            accountant.RegisteredCalculators.Add(
                new MonthlyFeeCalculator(new FeeDiscountCalculator(new FeeCalculator(), 0.1m))
                {
                    Merchant = new Merchant("TELIA")
                });
            accountant.RegisteredCalculators.Add(
                new MonthlyFeeCalculator(new FeeDiscountCalculator(new FeeCalculator(), 0.2m))
                {
                    Merchant = new Merchant("CIRCLE_K")
                });
        }

        private static void EnsureFileExistence()
        {
            var fileCreator = new FileCreator();

            fileCreator.CreateFileIfNotExists(TransactionsFilePath);
        }
    }
}
