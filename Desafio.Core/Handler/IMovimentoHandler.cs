using Desafio.Core.Models;
using Desafio.Core.Requests.Movimento;
using Desafio.Core.Responses;

namespace Desafio.Core.Handler
{
    public interface IMovimentoHandler
    {
        Task<Response<Movimento?>> CriarTransacaoAsync(CriarTransacaoRequest request);
        Task<Response<Movimento?>> CriarSaqueAsync(CriarSaqueRequest request);
        Task<Response<Movimento?>> CriarDepositoAsync(DepositoRequest request);
        Task<Response<Movimento?>> DeletarMovimentoAsync(DeletarMovimentoRequest request);
        Task<Response<Movimento?>> ObterMovimentoPorIdAsync(ObterMovimentoPorIdRequest request);
        Task<PagedResponse<List<Extrato>?>> ObterMovimentosPorContaAsync(ObterMovimentosPorContaRequest request);
    }
}
