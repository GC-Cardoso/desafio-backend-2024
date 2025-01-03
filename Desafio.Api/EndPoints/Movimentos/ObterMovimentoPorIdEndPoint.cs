using Desafio.Api.Common;
using Desafio.Core.Handler;
using Desafio.Core.Models;
using Desafio.Core.Requests.Movimento;
using Desafio.Core.Responses;

namespace Desafio.Api.EndPoints.Transacoes
{
    public class ObterMovimentoPorIdEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{contaId}/{movimentoId}", HandleAsync)
            .WithName("Obter Movimento")
            .WithSummary("Obtenção de Movimento.")
            .WithDescription("Retorna os dados de um movimento, seja ele transação, deposito ou saque.")
            .Produces<Response<Movimento>>()
            .Produces(StatusCodes.Status404NotFound);

        private static async Task<IResult> HandleAsync(
            IMovimentoHandler handler,
            int contaId,
            int movimentoId)
        {
            var request = new ObterMovimentoPorIdRequest
            {
                contaId = contaId,
                movimentoId = movimentoId
            };

            var result = await handler.ObterMovimentoPorIdAsync(request);
            return result.IsSucess ?
                     TypedResults.Ok(result.data) :
                     TypedResults.NotFound(result);

        }
    }
}
