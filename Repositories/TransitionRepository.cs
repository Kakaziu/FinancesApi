using FinancesApi.Data;
using FinancesApi.Models;

namespace FinancesApi.Repositories
{
    public class TransitionRepository : Repository<TransitionModel>, ITransitionRepository
    {
        public TransitionRepository(FinancesApiDbContext context) : base(context) { }
    }
}
