using System;
using Domain.Service.FeeCalculationStrategies;
using Domain.Service.Models;
using Models.Models;
using Persistence.FileSystem;
using Persistence.TransactionRepository;

namespace Domain.Service
{
    class Program
    {
        static void Main()
        {
            EnsureFileExistence();

            var transactionRepository = new TransactionRepository(new FileReader("../../../../transactions.txt"));
            var feeCalculator = GetFeeCalculatorWithRegisteredMerchants();
            CalculateAllFees(transactionRepository, feeCalculator);
        }

        private static void CalculateAllFees(TransactionRepository transactionRepository, FeeCalculator feeCalculator)
        {
            Transaction currentTransaction;
            while ((currentTransaction = transactionRepository.GetNextTransaction()) != null)
            {
                if (currentTransaction.MerchantName == string.Empty && currentTransaction.TransferAmount == 0)
                {
                    Console.WriteLine();
                    continue;
                }

                currentTransaction.Fee = feeCalculator.CalculateFee(currentTransaction);
                Console.WriteLine(currentTransaction);
            }
        }

        private static FeeCalculator GetFeeCalculatorWithRegisteredMerchants()
        {
            var feeCalculator = new FeeCalculator();
            feeCalculator.RegisteredMerchants.Add(new Merchant(new FeeWithDiscountCalculation(0.1m), "TELIA"));
            feeCalculator.RegisteredMerchants.Add(new Merchant(new FeeWithDiscountCalculation(0.2m), "CIRCLE_K"));
            return feeCalculator;
        }

        private static void EnsureFileExistence()
        {
            var fileCreator = new FileCreator();

            fileCreator.CreateFileIfNotExists("../../../../transactions.txt");
        }
    }
}
