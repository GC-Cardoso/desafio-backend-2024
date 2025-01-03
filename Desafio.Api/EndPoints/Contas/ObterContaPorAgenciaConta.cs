using Desafio.Api.Common;
using Desafio.Core.Handler;
using Desafio.Core.Models;
using Desafio.Core.Requests.Conta;
using Desafio.Core.Responses;

namespace Desafio.Api.EndPoints.Contas
{
    public class ObterContaPorAgenciaConta : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{numeroAgencia}/{numeroConta}", HandleAsync)
            .WithOrder(3)
            .WithName("3 - Obter Conta Por Dados")
            .WithSummary("Obtenção de conta pelos dados de agencia e conta.")
            .WithDescription("Retorna os dados de uma conta cujo a agencia e o numero da conta sejam iguais aos especificados na querry.")
            .Produces<Response<Conta>>()
            .Produces(StatusCodes.Status404NotFound);

         private static async Task<IResult> HandleAsync(
            IContaHandler handler,
            string numeroAgencia,
            string numeroConta)
        {
            var request = new ObterContaPorAgenciaContaRequest
            {
                numeroAgencia = numeroAgencia,
                numeroConta = numeroConta
            };

            var result = await handler.ObterPorAgenciaContaAsync(request);
            return result.IsSucess ?
                     TypedResults.Ok(result.data) :
                     TypedResults.NotFound(result);
        }




    }
}
