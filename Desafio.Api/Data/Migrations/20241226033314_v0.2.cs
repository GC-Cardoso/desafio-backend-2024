using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio.Api.Migrations
{
    /// <inheritdoc />
    public partial class v02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "saldo",
                table: "Contas",
                type: "DECIMAL(10,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(float),
                oldType: "DECIMAL(10,2)",
                oldDefaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "saldo",
                table: "Contas",
                type: "DECIMAL(10,2)",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(10,2)",
                oldDefaultValue: 0m);
        }
    }
}
