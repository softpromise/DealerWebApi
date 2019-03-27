using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartDealer.Models.Models.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDealer.Repository.ModelConfiguration
{
    public class CustomerAttributesModelConfiguration : IEntityTypeConfiguration<CustomerAttributes>
    {
        public void Configure(EntityTypeBuilder<CustomerAttributes> builder)
        {
            builder.HasKey(t => new { t.Id });

            builder.HasOne(c => c.IdentityProfile)
                   .WithOne(b => b.CustomerAttributes)
                   .HasForeignKey<IdentityProfile>(b => b.FKCustomerAttributesId);
        }
    }
}
