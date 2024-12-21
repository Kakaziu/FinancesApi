using FinancesApi.Models;

namespace FinancesApi.Repositories
{
    public interface ITransitionRepository : IRepository<TransitionModel>
    {
        Task<IEnumerable<TransitionModel>> GetAll();
    }
}
