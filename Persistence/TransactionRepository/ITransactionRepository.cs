using Persistence.Read_Models;

namespace Persistence.TransactionRepository
{
    public interface ITransactionRepository
    {
        Transaction GetNextTransaction();
    }
}
