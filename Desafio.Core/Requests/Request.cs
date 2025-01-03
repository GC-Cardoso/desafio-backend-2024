using System.ComponentModel.DataAnnotations;

namespace Desafio.Core.Requests
{
    public abstract class Request
    {
        public int contaId { get; set; }
    }
}
