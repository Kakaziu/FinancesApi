using FinancesApi.Data;
using FinancesApi.Models;
using FinancesApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinancesApi.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly FinancesApiDbContext _context;

        public TransactionRepository(FinancesApiDbContext context)
        {   
            _context = context;
        }

        public async Task<List<TransactionModel>> FindAll()
        {
            return await _context.Transactions.Include(x => x.User).ToListAsync();
        }

        public async Task<TransactionModel> FindById(int id)
        {
            return await _context.Transactions.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TransactionModel> Insert(TransactionModel transaction)
        {
            transaction.TransactionDate = DateTime.Now;
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<TransactionModel> Update(TransactionModel transaction, int id)
        {
            var updatedTransaction = await FindById(id);

            if (updatedTransaction == null) throw new Exception("Transação não encontrada");

            updatedTransaction.Description = transaction.Description;
            updatedTransaction.Value = transaction.Value;
            updatedTransaction.TransactionType = transaction.TransactionType;
            updatedTransaction.TransactionDate = DateTime.Now;

            _context.Transactions.Update(updatedTransaction);
            await _context.SaveChangesAsync();
            return updatedTransaction;
        }

        public async Task<TransactionModel> Delete(int id)
        {
            var transaction = await FindById(id);

            if (transaction == null) throw new Exception("Transaction não encontrado");

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return transaction;

        }
    }
}
