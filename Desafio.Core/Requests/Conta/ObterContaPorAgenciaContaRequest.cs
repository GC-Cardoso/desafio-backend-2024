using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Core.Requests.Conta
{
    public class ObterContaPorAgenciaContaRequest : Request
    {
        public string numeroAgencia { get; set; } = string.Empty;
        public string numeroConta { get; set; } = string.Empty;
    }
}
