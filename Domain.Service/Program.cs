using System;
using Models;
using Persistence.FileSystem;

namespace Domain.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            var transactionRepository = new TransactionRepository(new FileReader());
            Transaction currentTransaction;

            while ((currentTransaction = transactionRepository.GetNextTransaction()) != null)
            {
                currentTransaction.Fee = FeeCalculator.CalculateFee(currentTransaction);
                Console.WriteLine(currentTransaction);
            }
        }
    }
}
