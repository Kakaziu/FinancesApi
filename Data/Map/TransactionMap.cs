using FinancesApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancesApi.Data.Map
{
    public class TransactionMap : IEntityTypeConfiguration<TransactionModel>
    {
        public void Configure(EntityTypeBuilder<TransactionModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Value).IsRequired();
            builder.HasOne(x => x.User);
        }
    }
}
