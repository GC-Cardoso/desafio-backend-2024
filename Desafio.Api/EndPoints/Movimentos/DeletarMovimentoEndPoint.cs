using Desafio.Api.Common;
using Desafio.Core.Handler;
using Desafio.Core.Models;
using Desafio.Core.Requests.Movimento;
using Desafio.Core.Responses;

namespace Desafio.Api.EndPoints.Transacoes
{
    public class DeletarMovimentoEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{contaId}/{movimentoId}",HandleAsync)
            .WithDisplayName("Deletar Movimento")
            .WithSummary("Exclusão de depositos, saques ou transferencias")
            .WithDescription("Deleta movimentos de qualquer tipo com base na identificação da conta e do movimento, tambem altera o valor em conta dos afetados")
            .Produces<Response<Movimento?>>()
            .Produces(StatusCodes.Status400BadRequest);

        private static async Task<IResult> HandleAsync(
            IMovimentoHandler handler,
            int contaId,
            int movimentoId
            )
        {
            var request = new DeletarMovimentoRequest
            {
                contaId = contaId,
                movimentoId = movimentoId
            };

            var result = await handler.DeletarMovimentoAsync(request);
            return result.IsSucess ?
                     TypedResults.Ok(result.data) :
                     TypedResults.BadRequest(result);
        }
    }
}
