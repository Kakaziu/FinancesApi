using FinancesApi.Models;

namespace FinancesApi.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<TransactionModel>> FindAll();
        Task<TransactionModel> FindById(int id);
        Task<TransactionModel> Insert(TransactionModel transaction);
        Task<TransactionModel> Update(TransactionModel transaction, int id);
        Task<TransactionModel> Delete(int id);
    }
}
