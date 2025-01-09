using FinancesApi.Data;
using FinancesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancesApi.Repositories
{
    public class TransitionRepository : Repository<TransitionModel>, ITransitionRepository
    {
        public TransitionRepository(FinancesApiDbContext context) : base(context) { }

        public async Task<IEnumerable<TransitionModel>> GetAll()
        {
            return await _context.Transitions.AsNoTracking().Include(t => t.User).ToListAsync();
        }
    }
}
