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
            builder.HasOne(k => k.User)
                .WithMany(k => k.Bids)
                .HasForeignKey(k => k.UserId);

            builder.HasOne(k => k.Lot)
                .WithMany(k => k.Bids)
                .HasForeignKey(k => k.LotId);


            builder.Property(b => b.Amount)
                .IsRequired();

        }
    }
}
