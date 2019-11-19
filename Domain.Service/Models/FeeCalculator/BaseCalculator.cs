using Persistence.Read_Models;

namespace Domain.Service.Models.FeeCalculator
{
    public abstract class BaseCalculator
    {
        public Merchant Merchant { get; set; }

        public abstract decimal CalculateFee(Transaction transaction);
    }
}
