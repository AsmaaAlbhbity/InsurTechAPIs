using InsurTech.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurTech.Repository.Configuration
{
    public class HomePlanRequestConfiguration : IEntityTypeConfiguration<HomePlanRequest>
    {
        public void Configure(EntityTypeBuilder<HomePlanRequest> builder)
        {
            builder.Property(a => a.AttemptedTheft).HasColumnType("decimal(18,2)");
            builder.Property(a => a.WaterDamage).HasColumnType("decimal(18,2)");
            builder.Property(a => a.GlassBreakage).HasColumnType("decimal(18,2)");
            builder.Property(a => a.NaturalHazard).HasColumnType("decimal(18,2)");
            builder.Property(a => a.FiresAndExplosion).HasColumnType("decimal(18,2)");
        }
    }
}


