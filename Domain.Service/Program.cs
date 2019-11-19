using System;
using Domain.Service.Calculators;
using Domain.Service.Calculators.Decorators;
using Domain.Service.Models;
using Persistence.FileSystem;
using Persistence.Read_Models;
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
            CalculateAllFees(transactionRepository, feeAccountant);
        }

        private static void CalculateAllFees(TransactionRepository transactionRepository, Accountant feeAccountant)
        {
            Transaction currentTransaction;
            while ((currentTransaction = transactionRepository.GetNextTransaction()) != null)
            {
                if (currentTransaction.MerchantName == string.Empty && currentTransaction.TransferAmount == 0)
                {
                    Console.WriteLine();
                    continue;
                }

                currentTransaction.Fee = feeAccountant.CalculateFee(currentTransaction);
                Console.WriteLine(currentTransaction);
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

            fileCreator.CreateFileIfNotExists("../../../../transactions.txt");
        }
    }
}
