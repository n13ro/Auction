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

            builder.Property(k => k.NickName)
                .IsRequired()
                .HasMaxLength(20);

            //users -> lots
            builder.HasMany(u => u.Lots)
                .WithOne()
                .HasForeignKey("UserId");
        }
    }
}
