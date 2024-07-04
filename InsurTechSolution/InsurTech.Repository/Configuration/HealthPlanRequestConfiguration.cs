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
    public class HealthPlanRequestConfiguration : IEntityTypeConfiguration<HealthPlanRequest>
    {
        public void Configure(EntityTypeBuilder<HealthPlanRequest> builder)
        {
            builder.Property(a => a.ClinicsCoverage).HasColumnType("decimal(18,2)");
            builder.Property(a => a.HospitalizationAndSurgery).HasColumnType("decimal(18,2)");
            builder.Property(a => a.OpticalCoverage).HasColumnType("decimal(18,2)");
            builder.Property(a => a.DentalCoverage).HasColumnType("decimal(18,2)");
        }
    }
}

