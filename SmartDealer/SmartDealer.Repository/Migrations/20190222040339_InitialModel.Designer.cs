﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartDealer.Repository.Contexts;

namespace SmartDealer.Repository.Migrations
{
    [DbContext(typeof(DealerContext))]
    [Migration("20190222040339_InitialModel")]
    partial class InitialModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SmartDealer.Models.Models.Customer.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("SmartDealer.Models.Models.Customer.CustomerAttributes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AllowsEmailContact");

                    b.Property<bool>("AllowsMailContact");

                    b.Property<bool>("AllowsPhoneContact");

                    b.Property<bool>("AllowsSmsContact");

                    b.Property<string>("Comments");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CustomerNumber");

                    b.Property<string>("CustomerType");

                    b.Property<string>("DriverLicenseNumber");

                    b.Property<int>("DriverLicenseState");

                    b.Property<int>("FKCustomerId");

                    b.Property<bool>("IsMarried");

                    b.Property<bool>("IsWholesale");

                    b.Property<DateTime?>("LastContactDate");

                    b.Property<int>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.HasKey("Id");

                    b.HasIndex("FKCustomerId")
                        .IsUnique();

                    b.ToTable("CustomerAttributes");
                });

            modelBuilder.Entity("SmartDealer.Models.Models.Customer.CustomerName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("FKIdentityProfileId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("MiddleName");

                    b.Property<int>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("SingularName");

                    b.Property<string>("Suffix");

                    b.Property<int>("Title");

                    b.HasKey("Id");

                    b.HasIndex("FKIdentityProfileId")
                        .IsUnique();

                    b.ToTable("CustomerNames");
                });

            modelBuilder.Entity("SmartDealer.Models.Models.Customer.IdentityProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address1");

                    b.Property<string>("Address2");

                    b.Property<DateTime?>("BirthDate");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("County");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("EmailAddress");

                    b.Property<int>("FKCustomerAttributesId");

                    b.Property<int>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int>("Sex");

                    b.Property<string>("Ssn");

                    b.Property<string>("State");

                    b.Property<string>("Zip");

                    b.HasKey("Id");

                    b.HasIndex("FKCustomerAttributesId")
                        .IsUnique();

                    b.ToTable("IdentityProfiles");
                });

            modelBuilder.Entity("SmartDealer.Models.Models.Customer.PhoneNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Digits");

                    b.Property<int>("FKIdentityProfileId");

                    b.Property<int>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int>("NumberType");

                    b.HasKey("Id");

                    b.HasIndex("FKIdentityProfileId");

                    b.ToTable("PhoneNumbers");
                });

            modelBuilder.Entity("SmartDealer.Models.Models.Customer.CustomerAttributes", b =>
                {
                    b.HasOne("SmartDealer.Models.Models.Customer.Customer", "Customer")
                        .WithOne("CustomerAttributes")
                        .HasForeignKey("SmartDealer.Models.Models.Customer.CustomerAttributes", "FKCustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmartDealer.Models.Models.Customer.CustomerName", b =>
                {
                    b.HasOne("SmartDealer.Models.Models.Customer.IdentityProfile", "IdentityProfile")
                        .WithOne("CustomerName")
                        .HasForeignKey("SmartDealer.Models.Models.Customer.CustomerName", "FKIdentityProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmartDealer.Models.Models.Customer.IdentityProfile", b =>
                {
                    b.HasOne("SmartDealer.Models.Models.Customer.CustomerAttributes", "CustomerAttributes")
                        .WithOne("IdentityProfile")
                        .HasForeignKey("SmartDealer.Models.Models.Customer.IdentityProfile", "FKCustomerAttributesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmartDealer.Models.Models.Customer.PhoneNumber", b =>
                {
                    b.HasOne("SmartDealer.Models.Models.Customer.IdentityProfile", "IdentityProfile")
                        .WithMany("PhoneNumbers")
                        .HasForeignKey("FKIdentityProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
