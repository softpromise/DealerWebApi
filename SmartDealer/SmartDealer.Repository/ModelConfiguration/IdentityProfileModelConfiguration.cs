using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartDealer.Models.Models.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDealer.Repository.ModelConfiguration
{
    public class IdentityProfileModelConfiguration : IEntityTypeConfiguration<IdentityProfile>
    {
        public void Configure(EntityTypeBuilder<IdentityProfile> builder)
        {
            builder.HasKey(t => new { t.Id });

            builder.HasOne(c => c.CustomerName)
                   .WithOne(b => b.IdentityProfile)
                   .HasForeignKey<CustomerName>(b => b.FKIdentityProfileId);

            builder.HasMany(c => c.PhoneNumbers)
                   .WithOne(b => b.IdentityProfile)
                   .HasForeignKey(c => c.FKIdentityProfileId);
                   
        }
    }
}
