using Desafio.Api.Common;
using Desafio.Core.Handler;
using Desafio.Core.Models;
using Desafio.Core.Requests.Movimento;
using Desafio.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Api.EndPoints.Movimentos
{
    public class CriarDepositoEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/Depositar", HandleAsync)
            .WithDisplayName("Criar Deposito")
            .WithSummary("Criar movimentação do tipo Deposito.")
            .WithDescription("Cria os dados de uma nova movimentação do tipo deposito, e altera o valor do saldo em conta.")
            .Produces<Response<Movimento?>>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        private static async Task<IResult> HandleAsync(
            IMovimentoHandler handler,
            [FromQuery] string agencia,
            [FromQuery] string conta,
            [FromQuery] decimal valor)
        {
            var depositoRequest = new DepositoRequest
            {
                agencia = agencia,
                conta = conta,
                valor = valor
            };

            var result = await handler.CriarDepositoAsync(depositoRequest);

            return result.IsSucess ?
                TypedResults.Created($"{result.data?.contaId}/{result.data?.movimentoId}",result.data) :
                TypedResults.NotFound(result);


        }
    }
}
