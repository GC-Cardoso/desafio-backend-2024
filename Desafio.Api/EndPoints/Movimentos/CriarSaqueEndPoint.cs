using Desafio.Api.Common;
using Desafio.Core.Handler;
using Desafio.Core.Models;
using Desafio.Core.Requests.Movimento;
using Desafio.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Api.EndPoints.Transacoes
{
    public class CriarSaqueEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/{contaId}/Saque", HandleAsync)
           .WithName("Criar Saque")
           .WithSummary("Criar movimentação do tipo Saque.")
           .WithDescription("Cria os dados de uma nova movimentação do tipo saque, e altera o valor do saldo em conta.")
           .Produces<Response<Movimento?>>(StatusCodes.Status201Created)
           .Produces(StatusCodes.Status400BadRequest);



        private static async Task<IResult> HandleAsync(
        IMovimentoHandler handler,
        int contaId,
        [FromQuery] decimal valor)
    {
            var request = new CriarSaqueRequest
            {
                valor = valor,
                contaId = contaId
            };

        var result = await handler.CriarSaqueAsync(request);

        return result.IsSucess ?
                  TypedResults.Created($"{ result.data?.contaId}/{ result.data?.movimentoId}", result.data) :
                  TypedResults.BadRequest(result);
    }
}
}
