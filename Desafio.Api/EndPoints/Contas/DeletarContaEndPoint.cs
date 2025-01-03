using Desafio.Api.Common;
using Desafio.Core.Handler;
using Desafio.Core.Models;
using Desafio.Core.Requests.Conta;
using Desafio.Core.Responses;

namespace Desafio.Api.EndPoints.Contas
{
    public class DeletarContaEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
       => app.MapDelete("/{contaId}", HandleAsync)
           .WithName("Deletar Conta")
           .WithSummary("exclusão de conta.")
           .WithDescription("Apaga os dados de uma conta.")
           .Produces<Response<Conta>>();

        private static async Task<IResult> HandleAsync(
             IContaHandler handler,
            int contaId)
        {
            var request = new DeletarContaRequest
            { contaId = contaId };

            var result = await handler.DeletarAsync(request);
            return result.IsSucess ?
                     TypedResults.Ok( result.data) :
                     TypedResults.BadRequest(result);
        }
    }
}
