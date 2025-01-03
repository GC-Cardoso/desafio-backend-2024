using Desafio.Api.Common;
using Desafio.Core.Handler;
using Desafio.Core.Models;
using Desafio.Core.Requests;
using Desafio.Core.Requests.Conta;
using Desafio.Core.Responses;

namespace Desafio.Api.EndPoints.Contas
{
    public class ObterContaPorIdEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithOrder(2)
            .WithName("2 - Obter Conta")
            .WithSummary("Obtenção de conta.")
            .WithDescription("Retorna os dados de uma conta.")
            .Produces<Response<Conta>>()
            .Produces(StatusCodes.Status404NotFound);

        private static async Task<IResult> HandleAsync(
            IContaHandler handler,
            int contaId)
        {
            var request = new ObterContaRequest { contaId = contaId };
            var result = await handler.ObterPorIdAsync(request);
            return result.IsSucess ?
                     TypedResults.Ok( result.data) :
                     TypedResults.NotFound(result);
        }
    }
}
