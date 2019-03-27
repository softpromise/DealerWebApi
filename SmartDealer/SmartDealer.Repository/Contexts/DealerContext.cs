using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SmartDealer.Models.Models.Customer;
using SmartDealer.Models.Models.User;
using SmartDealer.Repository.ModelConfiguration;

namespace SmartDealer.Repository.Contexts
{
    public class DealerContext : DbContext
    {
        public DealerContext(DbContextOptions<DealerContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAttributes> CustomerAttributes { get; set; }
        public DbSet<IdentityProfile> IdentityProfiles { get; set; }
        public DbSet<CustomerName> CustomerNames { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customizations must go after base.OnModelCreating(builder)
            modelBuilder.ApplyConfiguration(new UserModelConfiguration());


            modelBuilder.ApplyConfiguration(new CustomerModelConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerAttributesModelConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityProfileModelConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerNameModelConfiguration());
            modelBuilder.ApplyConfiguration(new PhoneNumberModelConfiguration());

        }
    }
}
