using Domain.Service.Models;
using Persistence.Read_Models;

namespace Domain.Service.Calculators
{
    public abstract class BaseCalculator
    {
        public Merchant Merchant { get; set; }

        public abstract decimal CalculateFee(Transaction transaction);
    }
}
