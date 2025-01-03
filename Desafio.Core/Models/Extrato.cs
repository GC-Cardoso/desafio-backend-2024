using Desafio.Core.Enums;

namespace Desafio.Core.Models
{
    public class Extrato
    {
        public int movimentoId { get; set; }
        public DateTime dataCriacao { get; set; }
        public string descricaoMovimento { get; set; } = string.Empty;
        public decimal valor {  get; set; }
        public string participante { get; set; } = string.Empty;


        
    }
}
