using Domain.Service.Calculators;
using Persistence.Read_Models;

namespace UnitTests.TestModels
{
    class BaseTestCalculator : BaseCalculator
    {
        public override decimal CalculateFee(Transaction transaction)
        {
            return transaction.Fee;
        }
    }
}
