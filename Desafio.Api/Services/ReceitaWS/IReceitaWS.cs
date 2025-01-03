using Desafio.Core.Responses;

namespace Desafio.Api.Services.ReceitaWS
{
    public interface IReceitaWS
    {
        public Task<Response<ResponseApi?>> ObterCnpjInfo(string cnpj);
    }
}
