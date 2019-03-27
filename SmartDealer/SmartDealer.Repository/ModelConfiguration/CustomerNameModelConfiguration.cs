using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartDealer.Models.Models.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDealer.Repository.ModelConfiguration
{
    public class CustomerNameModelConfiguration : IEntityTypeConfiguration<CustomerName>
    {
        public void Configure(EntityTypeBuilder<CustomerName> builder)
        {
            builder.HasKey(t => new { t.Id });

        }
    }
}
