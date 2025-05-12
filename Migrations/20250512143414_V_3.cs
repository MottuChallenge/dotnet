using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MottuGrid_Dotnet.Migrations
{
    /// <inheritdoc />
    public partial class V_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EngineType",
                table: "Motorcycles",
                type: "NVARCHAR2(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)",
                oldMaxLength: 50);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EngineType",
                table: "Motorcycles",
                type: "NUMBER(10)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(50)",
                oldMaxLength: 50);
        }
    }
}
