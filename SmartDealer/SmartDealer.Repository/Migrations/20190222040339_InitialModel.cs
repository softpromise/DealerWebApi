using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartDealer.Repository.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: false),
                    AllowsPhoneContact = table.Column<bool>(nullable: false),
                    AllowsMailContact = table.Column<bool>(nullable: false),
                    AllowsEmailContact = table.Column<bool>(nullable: false),
                    AllowsSmsContact = table.Column<bool>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    DriverLicenseNumber = table.Column<string>(nullable: true),
                    DriverLicenseState = table.Column<int>(nullable: false),
                    IsMarried = table.Column<bool>(nullable: false),
                    IsWholesale = table.Column<bool>(nullable: false),
                    LastContactDate = table.Column<DateTime>(nullable: true),
                    CustomerNumber = table.Column<string>(nullable: true),
                    CustomerType = table.Column<string>(nullable: true),
                    FKCustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerAttributes_Customers_FKCustomerId",
                        column: x => x.FKCustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Sex = table.Column<int>(nullable: false),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    County = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Ssn = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    FKCustomerAttributesId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityProfiles_CustomerAttributes_FKCustomerAttributesId",
                        column: x => x.FKCustomerAttributesId,
                        principalTable: "CustomerAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerNames",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    SingularName = table.Column<string>(nullable: true),
                    Title = table.Column<int>(nullable: false),
                    Suffix = table.Column<string>(nullable: true),
                    FKIdentityProfileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerNames_IdentityProfiles_FKIdentityProfileId",
                        column: x => x.FKIdentityProfileId,
                        principalTable: "IdentityProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: false),
                    NumberType = table.Column<int>(nullable: false),
                    Digits = table.Column<string>(nullable: true),
                    FKIdentityProfileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneNumbers_IdentityProfiles_FKIdentityProfileId",
                        column: x => x.FKIdentityProfileId,
                        principalTable: "IdentityProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAttributes_FKCustomerId",
                table: "CustomerAttributes",
                column: "FKCustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerNames_FKIdentityProfileId",
                table: "CustomerNames",
                column: "FKIdentityProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdentityProfiles_FKCustomerAttributesId",
                table: "IdentityProfiles",
                column: "FKCustomerAttributesId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_FKIdentityProfileId",
                table: "PhoneNumbers",
                column: "FKIdentityProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerNames");

            migrationBuilder.DropTable(
                name: "PhoneNumbers");

            migrationBuilder.DropTable(
                name: "IdentityProfiles");

            migrationBuilder.DropTable(
                name: "CustomerAttributes");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
