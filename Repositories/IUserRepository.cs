using FinancesApi.Models;

namespace FinancesApi.Repositories
{
    public interface IUserRepository : IRepository<UserModel>
    {
        Task<UserModel> GetByEmail(string email);
        Task<IEnumerable<UserModel>> GetAll();
    }
}
