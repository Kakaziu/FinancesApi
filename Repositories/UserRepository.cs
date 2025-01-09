using FinancesApi.Data;
using FinancesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancesApi.Repositories
{
    public class UserRepository : Repository<UserModel>, IUserRepository
    {
        public UserRepository(FinancesApiDbContext context) : base(context) { }

        public async Task<IEnumerable<UserModel>> GetAll()
        {
            return await _context.Users.AsNoTracking().Include(u => u.Transitions).ToListAsync();
        }

        public async Task<UserModel> GetByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null) throw new ArgumentNullException("Usuário não encontrado.");

            return user;
        }
    }
}
