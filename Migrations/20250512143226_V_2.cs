using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MottuGrid_Dotnet.Migrations
{
    /// <inheritdoc />
    public partial class V_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Street = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Number = table.Column<string>(type: "NVARCHAR2(7)", maxLength: 7, nullable: false),
                    Neighborhood = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    ZipCode = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Country = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branchs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    CNPJ = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: false),
                    Phone = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    AddressId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branchs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branchs_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Yards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Area = table.Column<double>(type: "BINARY_DOUBLE", precision: 10, scale: 2, nullable: false),
                    AddressId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    BranchId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Yards_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Yards_Branchs_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branchs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Color = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Area = table.Column<double>(type: "BINARY_DOUBLE", precision: 10, scale: 2, nullable: false),
                    YardId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_Yards_YardId",
                        column: x => x.YardId,
                        principalTable: "Yards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Motorcycles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Model = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    EngineType = table.Column<int>(type: "NUMBER(10)", maxLength: 50, nullable: false),
                    Plate = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false),
                    LastRevisionDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    SectionId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorcycles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Motorcycles_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Message = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    MotorcycleId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logs_Motorcycles_MotorcycleId",
                        column: x => x.MotorcycleId,
                        principalTable: "Motorcycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branchs_AddressId",
                table: "Branchs",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Logs_MotorcycleId",
                table: "Logs",
                column: "MotorcycleId");

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_SectionId",
                table: "Motorcycles",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_YardId",
                table: "Sections",
                column: "YardId");

            migrationBuilder.CreateIndex(
                name: "IX_Yards_AddressId",
                table: "Yards",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Yards_BranchId",
                table: "Yards",
                column: "BranchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Motorcycles");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Yards");

            migrationBuilder.DropTable(
                name: "Branchs");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
