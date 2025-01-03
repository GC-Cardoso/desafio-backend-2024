using Desafio.Core.Enums;

namespace Desafio.Core.Models
{
    public class Movimento
    {
        public int contaId { get; set; }
        public int movimentoId { get; set; }
        public decimal valor { get; set; }
        public ETipoMovimento tipoMovimento { get; set; }
        public int? contaAlvoId { get; set; }
        public DateTime dataCriacao {  get; set; }
    }
}
