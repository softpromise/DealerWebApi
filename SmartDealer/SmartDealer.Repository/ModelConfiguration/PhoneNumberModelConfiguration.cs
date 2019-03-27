using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartDealer.Models.Models.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDealer.Repository.ModelConfiguration
{
    public class PhoneNumberModelConfiguration : IEntityTypeConfiguration<PhoneNumber>
    {
        public void Configure(EntityTypeBuilder<PhoneNumber> builder)
        {
            builder.HasKey(t => new { t.Id });

            builder.HasOne(c => c.IdentityProfile)
                   .WithMany(b => b.PhoneNumbers)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
