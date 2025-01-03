using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio.Api.Migrations
{
    /// <inheritdoc />
    public partial class v05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<sbyte>(
                name: "tipoMovimento",
                table: "Transacoes",
                type: "TINYINT",
                nullable: false,
                oldClrType: typeof(ulong),
                oldType: "BIT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<ulong>(
                name: "tipoMovimento",
                table: "Transacoes",
                type: "BIT",
                nullable: false,
                oldClrType: typeof(sbyte),
                oldType: "TINYINT");
        }
    }
}
