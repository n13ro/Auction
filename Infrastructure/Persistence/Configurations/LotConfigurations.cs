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
    /// <summary>
    /// Связи ЛОТОВ
    /// </summary>
    public class LotConfigurations : IEntityTypeConfiguration<Lot>
    {
        public void Configure(EntityTypeBuilder<Lot> builder)
        {

            //builder.HasMany(k => k.Bids)
            //    .WithMany()
            //    .UsingEntity(j => j.ToTable("BidsLots"));
        }
    }
}
