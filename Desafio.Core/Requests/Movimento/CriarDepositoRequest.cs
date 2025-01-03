using System.ComponentModel.DataAnnotations;
namespace Desafio.Core.Requests.Movimento
{
    public class CriarDepositoRequest : Request
    {
        [Required(ErrorMessage = "É necessário informar o valor do depósito.")]
        public decimal valor { get; set; }

        [Required(ErrorMessage = "É necessário informar a agência do beneficiado.")]
        public string numeroAgenciaBeneficiario { get; set; } = string.Empty;

        [Required(ErrorMessage = "É necessário informar o número da conta do beneficiado.")]
        public string numeroContaBeneficiario { get; set; } = string.Empty;

    }
}
