using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartDealer.Models.Models.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDealer.Repository.ModelConfiguration
{
    public class CustomerModelConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(t => new { t.Id });

            builder.HasOne(c => c.CustomerAttributes)
                   .WithOne(b => b.Customer)
                   .HasForeignKey<CustomerAttributes>(b => b.FKCustomerId);
                   

        }
    }
}
