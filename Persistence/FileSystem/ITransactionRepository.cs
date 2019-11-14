using Models;

namespace Persistence.FileSystem
{
    public interface ITransactionRepository
    {
        Transaction GetNextTransaction();
    }
}
