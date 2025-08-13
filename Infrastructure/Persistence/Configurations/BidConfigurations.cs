using Domain.Bids;
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
    public class BidConfigurations : IEntityTypeConfiguration<Bid>
    {
        public void Configure(EntityTypeBuilder<Bid> builder)
        {
            builder.HasOne(k => k.user)
                .WithMany(k => k.Bids)
                .HasForeignKey(u => u.userId);

            builder.HasOne(k => k.lot)
                .WithMany(k => k.Bids)
                .HasForeignKey(k => k.lotId);

            builder.Property(b => b.Amount)
                .IsRequired();
        }
    }
}
