using FinancesApi.Data.Map;
using FinancesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancesApi.Data
{
    public class FinancesApiDbContext : DbContext
    {
        public FinancesApiDbContext(DbContextOptions<FinancesApiDbContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
