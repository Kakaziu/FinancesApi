using FinancesApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinancesApi.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly FinancesApiDbContext _context;

        public Repository(FinancesApiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            T? element = await _context.Set<T>().FirstOrDefaultAsync(predicate);

            if (element == null) throw new ArgumentNullException(nameof(element));

            return element;
        }

        public async Task<T> Create(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
