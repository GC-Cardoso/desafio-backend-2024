using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio.Api.Migrations
{
    /// <inheritdoc />
    public partial class v04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "transacaoId",
                table: "Transacoes",
                newName: "movimentoId");

            migrationBuilder.AddColumn<DateTime>(
                name: "dataCriacao",
                table: "Transacoes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dataCriacao",
                table: "Transacoes");

            migrationBuilder.RenameColumn(
                name: "movimentoId",
                table: "Transacoes",
                newName: "transacaoId");
        }
    }
}
