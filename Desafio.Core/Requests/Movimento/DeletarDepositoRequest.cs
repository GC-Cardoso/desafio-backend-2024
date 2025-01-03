using System.ComponentModel.DataAnnotations;

namespace Desafio.Core.Requests.Movimento
{
    public class DeletarDepositoRequest : Request
    {

        [Required(ErrorMessage = "É necessario inforar a movimentação a ser excluida")]
        public int movimentoId {  get; set; }
    }
}
