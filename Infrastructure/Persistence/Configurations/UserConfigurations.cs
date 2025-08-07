using Domain.Lots;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(k => k.Lots)
                .WithMany()
                .UsingEntity(j => j.ToTable("LotsUsers"));

            builder.HasMany(u => u.Bids)
                .WithMany()
                .UsingEntity(j => j.ToTable("BidsUsers"));

        }
    }
}
