namespace Desafio.Core.Models
{
    public class Conta
    {
        public int contaId { get; set; }
        public string nome { get; set; } = string.Empty;
        public string cnpj { get; set; } = string.Empty;
        public string numeroConta { get; set; } = string.Empty;
        public string agencia { get; set; } = string.Empty;
        public string imagemDocumento { get; set; } = string.Empty;
        public decimal saldo { get; set; } = 0m;

    }
}
