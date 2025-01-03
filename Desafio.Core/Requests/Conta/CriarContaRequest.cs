using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Desafio.Core.Requests.Conta
{
    public class CriarContaRequest : Request
    {
        [JsonIgnore]
        public new int contaId { get; set; }

        [Required(ErrorMessage = "É necessário informar o CNPJ.")]
        [MaxLength(18, ErrorMessage = "O cnpj deve conter no maximo 18 caracteres.")]
        public string cnpj { get; set; } = string.Empty;

        [Required(ErrorMessage = "É necessário incluir uma imagem.")]
        [MaxLength(255, ErrorMessage = "A URLdeve conter no maximo 255 caracteres.")]
        public string imagemDocumento { get; set; } = string.Empty;
    }
}
