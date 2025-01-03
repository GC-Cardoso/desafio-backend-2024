using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Core.Requests.Movimento
{
    public class CriarSaqueRequest : Request
    {
        [Required(ErrorMessage = "É necessario informar o valor do saque.")]
        public decimal valor { get; set; }
    }
}
