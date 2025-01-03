using Desafio.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Core.Requests.Movimento
{
    public class CriarTransacaoRequest : Request
    {


        [Required(ErrorMessage = "É necessario informar o valor da transação.")]
        public decimal valor { get; set; }

        [Required(ErrorMessage = "É necessario informar o tomador/beneficiario da transação.")]
        public string numeroContaBeneficiario { get; set; } = string.Empty;

        [Required(ErrorMessage = "É necessario informar o tomador/beneficiario da transação.")]
        public string numeroAgenciaBeneficiario { get; set; } = string.Empty;
    }
}
