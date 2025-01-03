using Desafio.Api.Common;
using Desafio.Core.Handler;
using Desafio.Core.Models;
using Desafio.Core.Requests.Movimento;
using Desafio.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Api.EndPoints.Transacoes
{
    public class CriarTransacaoEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/{contaId}", HandleAsync)
           .WithName("Criar transacao")
           .WithSummary("Criar transacao.")
           .WithDescription("Cria os dados de uma nova transferencia e altera os dados em ambas as contas.")
           .Produces<Response<Movimento?>>(StatusCodes.Status201Created)
           .Produces(StatusCodes.Status400BadRequest);



        private static async Task<IResult> HandleAsync(
            IMovimentoHandler handler,
            int contaId,
            [FromQuery] decimal valor,
            [FromQuery] string Agencia,
            [FromQuery] string Conta)
        {
            var request = new CriarTransacaoRequest
            {
                contaId = contaId,
                valor = valor,
                numeroAgenciaBeneficiario = Agencia,
                numeroContaBeneficiario = Conta
            };

            var result = await handler.CriarTransacaoAsync(request);

            return result.IsSucess ?
                      TypedResults.Created($"{result.data?.contaId}/{result.data?.movimentoId}", result.data) :
                      TypedResults.BadRequest(result);
        }
    }
}
