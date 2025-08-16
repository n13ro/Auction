using Domain.Lots;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Связи ПОЛЬЗОВАТЕЛЕЙ
    /// </summary>
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasMany(k => k.Lots)
                .WithMany()
                .UsingEntity(j => j.ToTable("LotsUsers"));


        }
    }
}
