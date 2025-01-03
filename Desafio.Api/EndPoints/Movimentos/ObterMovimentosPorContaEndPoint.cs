using Desafio.Api.Common;
using Desafio.Core;
using Desafio.Core.Handler;
using Desafio.Core.Models;
using Desafio.Core.Requests.Movimento;
using Desafio.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Api.EndPoints.Transacoes
{
    public class ObterMovimentosPorContaEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{idConta}", HandleAsync)
            .WithDisplayName("Extrato")
            .WithSummary("Obtenção de todos os movimentos de uma conta.")
            .WithDescription("Retorna os dados de todos os movimento de uma conta por paginação, ordenados por data, sejam eles transações, depositos ou saques.")
            .Produces<PagedResponse<List<Movimento>?>>()
            .Produces(StatusCodes.Status400BadRequest);

        private static async Task<IResult> HandleAsync(
            IMovimentoHandler handler,
            int idConta,
            [FromQuery] int pageNumber = Configuration.DEFAULT_PAGE_NUMBER,
            [FromQuery] int pageSize = Configuration.DEFAULT_PAGE_SIZE
            )
        {
            var request = new ObterMovimentosPorContaRequest
            {
                contaId = idConta,
                pageNumber = pageNumber,
                pageSize = pageSize,
                
            };

            var result = await handler.ObterMovimentosPorContaAsync(request);

            return result.IsSucess ?
                TypedResults.Ok(result.data):
                TypedResults.BadRequest(result);
        }
    }
}
