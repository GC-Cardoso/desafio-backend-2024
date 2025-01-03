using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Desafio.Core.Requests.Conta
{
    public class ObterContaRequest : Request
    {
        [JsonIgnore]
        public new int contaId { get; set; }
    }
}
