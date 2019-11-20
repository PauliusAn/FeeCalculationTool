using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.Service.Calculators;
using Persistence.FileSystem;
using Persistence.Read_Models;
using Persistence.TransactionRepository;
using TechTalk.SpecFlow;
using Xunit;

namespace AcceptanceTests.Steps
{
    [Binding]
    class FeeCalculationSteps
    {
        private List<Transaction> _madeTransactions = new List<Transaction>();
        private List<Transaction> _calculatedTransactions = new List<Transaction>();
        private const string TransactionsFilePath = "../../../transactions.txt";

        [Given("That following transactions are made")]
        public void GivenThatFollowingTransactionsAreMade(Table transactionsTable)
        {
            File.Create(TransactionsFilePath).Close();

            using (var writer = new StreamWriter(TransactionsFilePath))
            {
                foreach (var row in transactionsTable.Rows)
                {
                    writer.WriteLine($"{row["date"]} {row["merchantName"]} {row["amount"]}");
                }
            }
        }

        [When("fees calculation app is executed")]
        public void WhenFeesCalculationAppIsExecuted()
        {
            var transactionRepository = new TransactionRepository(new FileReader(TransactionsFilePath));
            var feeAccountant = new Accountant();

            _calculatedTransactions = feeAccountant.CalculateAllFees(transactionRepository).ToList();
        }

        [Then("the output is")]
        public void ThenTheOutputIs(Table resultTable)
        {
            foreach (var resultTableRow in resultTable.Rows)
            {
                var transaction = new Transaction(
                    DateTime.Parse(resultTableRow["date"]),
                    resultTableRow["merchantName"], 0)
                {
                    Fee = decimal.Parse(resultTableRow["fee"])
                };

                var result = _calculatedTransactions.Single(x => x.MerchantName == resultTableRow["merchantName"] &&
                                                                x.Date == DateTime.Parse(resultTableRow["date"]));

                Assert.Equal(transaction.MerchantName, result.MerchantName);
                Assert.Equal(transaction.Date, result.Date);
                Assert.Equal(transaction.Fee, result.Fee);
            }
        }
    }
}
