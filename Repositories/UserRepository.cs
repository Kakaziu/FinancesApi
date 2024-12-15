using FinancesApi.Data;
using FinancesApi.Models;

namespace FinancesApi.Repositories
{
    public class UserRepository : Repository<UserModel>, IUserRepository
    {
        public UserRepository(FinancesApiDbContext context) : base(context) { }
    }
}
