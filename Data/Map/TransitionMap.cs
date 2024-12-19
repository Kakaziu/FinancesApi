using FinancesApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancesApi.Data.Map
{
    public class TransitionMap : IEntityTypeConfiguration<TransitionModel>
    {
        public void Configure(EntityTypeBuilder<TransitionModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Value).IsRequired();
            builder.Property(x => x.TransitionType).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.HasOne(x => x.User);
        }
    }
}
