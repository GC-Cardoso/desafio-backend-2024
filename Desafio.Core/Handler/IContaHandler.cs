using Desafio.Api.Services.Files;
using Desafio.Core.Models;
using Desafio.Core.Requests.Conta;
using Desafio.Core.Responses;

namespace Desafio.Core.Handler
{
    public interface IContaHandler
    {
        Task<Response<Conta?>> CriarAsync(CriarContaRequest request, FileService fileService);
        Task<Response<Conta?>> AlterarAsync(AlterarContaRequest request);
        Task<Response<Conta?>> DeletarAsync(DeletarContaRequest request);
        Task<Response<Conta?>> ObterPorIdAsync(ObterContaRequest request);
        Task<PagedResponse<List<Conta>?>> ObterTudoAsync(ObterTudoContaRequest request);
        Task<Response<Conta?>> ObterPorAgenciaContaAsync(ObterContaPorAgenciaContaRequest request);


    }
}
