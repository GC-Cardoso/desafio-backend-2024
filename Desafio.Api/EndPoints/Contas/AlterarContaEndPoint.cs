using Desafio.Api.Common;
using Desafio.Core.Handler;
using Desafio.Core.Models;
using Desafio.Core.Requests.Conta;
using Desafio.Core.Responses;

namespace Desafio.Api.EndPoints.Contas
{
    public class AlterarContaEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Alterar Conta")
            .WithSummary("alteração de conta.")
            .WithDescription("Altera os dados de uma conta.")
            .Produces<Response<Conta?>>()
            .Produces(StatusCodes.Status400BadRequest);

        private static async Task<IResult> HandleAsync(
            IContaHandler handler,
            AlterarContaRequest request,
            int contaId)
        {
            request.contaId = contaId;

            var result = await handler.AlterarAsync(request);
            return result.IsSucess?
                     TypedResults.Ok(result.data) :
                     TypedResults.BadRequest(result);
        }
    }


}
