using FinancesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancesApi.Data
{
    public class FinancesApiDbContext : DbContext
    {
        public FinancesApiDbContext(DbContextOptions<FinancesApiDbContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
    }
}
