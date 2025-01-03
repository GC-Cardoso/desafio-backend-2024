using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio.Api.Migrations
{
    /// <inheritdoc />
    public partial class v101 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "saldo",
                table: "Contas",
                type: "DECIMAL(10,2)",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "Transacoes",
                columns: table => new
                {
                    transacaoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    contaId = table.Column<int>(type: "INTEGER", nullable: false),
                    valor = table.Column<float>(type: "DECIMAL(10,2)", nullable: false),
                    tipoMovimento = table.Column<ulong>(type: "BIT", nullable: false),
                    contaAlvoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacoes", x => x.transacaoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transacoes");

            migrationBuilder.DropColumn(
                name: "saldo",
                table: "Contas");
        }
    }
}
