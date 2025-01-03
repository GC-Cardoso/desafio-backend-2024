using System.ComponentModel.DataAnnotations;

namespace Desafio.Core.Requests.Movimento
{
    public class ObterMovimentoPorIdRequest : Request
    {

        [Required(ErrorMessage = "É necessario informar o id da transação.")]
        public int movimentoId { get; set; }

    }
}
