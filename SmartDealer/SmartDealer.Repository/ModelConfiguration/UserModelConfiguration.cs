using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartDealer.Models.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDealer.Repository.ModelConfiguration
{
    public class UserModelConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(t => new { t.Id });
        }
    }
}
