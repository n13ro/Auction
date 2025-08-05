using Domain.Lots;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class LotConfigurations : IEntityTypeConfiguration<Lot>
    {
        public void Configure(EntityTypeBuilder<Lot> builder)
        {
            builder.HasOne<User>()
                .WithMany(u => u.Lots)
                .HasForeignKey("UserId");

            builder.HasMany(u => u.Bids)
                .WithOne()
                .HasForeignKey(b => b.LotId);
        }
    }
}
