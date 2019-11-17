using Models.Models;

namespace Persistence.TransactionRepository
{
    public interface ITransactionRepository
    {
        Transaction GetNextTransaction();
    }
}
