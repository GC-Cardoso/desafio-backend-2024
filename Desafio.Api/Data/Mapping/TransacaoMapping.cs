using Desafio.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Api.Data.Mapping
{
    public class TransacaoMapping:IEntityTypeConfiguration<Movimento>
    {
        public void Configure(EntityTypeBuilder<Movimento> builder)
        {
            builder.ToTable("Transacoes");
            builder.HasKey(x => x.movimentoId);

            builder.Property(x => x.valor)
                .IsRequired()
                .HasColumnType("DECIMAL(10, 2)");

            builder.Property(x => x.tipoMovimento)
                .IsRequired()
                .HasColumnType("TINYINT");

            builder.Property(x => x.contaId)
                .IsRequired()
                .HasColumnType("INTEGER");

            builder.Property(x => x.contaAlvoId)
                .IsRequired(false)
                .HasColumnType("INTEGER");


        }
       
    }
}
