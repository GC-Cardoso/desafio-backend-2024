namespace Desafio.Core.Requests.Movimento
{
    public class DepositoRequest : Request
    {
        public decimal valor {  get; set; }
        public string conta { get; set; } = string.Empty;
        public string agencia { get; set; } = string.Empty;
    }
}
