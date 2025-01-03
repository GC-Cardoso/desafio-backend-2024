using Desafio.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Api.Data.Mapping
{
    public class ContaMapping : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.ToTable("Contas");

            builder.HasKey(x => x.contaId);

            builder.Property(x => x.nome)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(125);

            builder.Property(x => x.cnpj)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(18);

            builder.Property(x => x.numeroConta)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(10);

            builder.Property(x => x.agencia)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(4);

            builder.Property(x => x.imagemDocumento)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255);

            builder.Property(x => x.saldo)
                .IsRequired()
                .HasColumnType("DECIMAL(10, 2)")
                .HasDefaultValue(0.00);
        }
    }
}
