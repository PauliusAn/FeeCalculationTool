using System;
using System.IO;
using System.Linq;
using Models.Models;
using Persistence.FileSystem;

namespace Persistence.TransactionRepository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IFileReader _transactionFileReader;

        public TransactionRepository(IFileReader transactionFileReader)
        {
            _transactionFileReader = transactionFileReader;
        }

        public Transaction GetNextTransaction()
        {
            var transactionString = _transactionFileReader.ReadNextLine();

            // To keep output formatted as input, every empty line in source file is represented by
            // Transaction object with merchantName as empty string and transferAmount as 0
            if (transactionString == "")
            {
                return new Transaction(DateTime.Now, string.Empty, 0);
            }

            if (transactionString == null)
            {
                return null;
            }

            var lineParts = transactionString.Split(' ');
            var transactionInfo = lineParts.Where(x => x != string.Empty).ToList();

            if (transactionInfo.Count != 3)
            {
                throw new InvalidDataException("Transaction in transactions file was in invalid format");
            }

            return new Transaction(ParseDate(transactionInfo[0]), transactionInfo[1], decimal.Parse(transactionInfo[2]));
        }

        private static DateTime ParseDate(string dateString)
        {
            var dateParts = dateString.Split('-');
            if (dateParts.Length != 3)
            {
                throw new InvalidDataException("Transaction in transactions file was in invalid format");
            }

            var year = short.Parse(dateParts[0]);
            var month = short.Parse(dateParts[1]);
            var day = short.Parse(dateParts[2]);

            return new DateTime(year, month, day);
        }

        private string GetTransactionLine()
        {
            var fileLine = _transactionFileReader.ReadNextLine();

            while (fileLine == string.Empty)
            {
                fileLine = _transactionFileReader.ReadNextLine();
            }

            return fileLine;
        }
    }
}
