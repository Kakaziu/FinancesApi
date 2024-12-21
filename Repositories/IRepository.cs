using System.Linq.Expressions;

namespace FinancesApi.Repositories
{
    public interface IRepository<T>
    {
        Task<T> Get(Expression<Func<T, bool>> predicate);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);
    }
}
