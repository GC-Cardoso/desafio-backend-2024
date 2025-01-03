using Desafio.Api.Common;
using Desafio.Core;
using Desafio.Core.Handler;
using Desafio.Core.Models;
using Desafio.Core.Requests.Conta;
using Desafio.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Api.EndPoints.Contas
{
    public class ObterContasEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
       => app.MapGet("/", HandleAsync)
           .WithName("Obter contas")
           .WithSummary("Obtenção de contas.")
           .WithDescription("Retorna os dados de todas as contas paginadas em 25 itens.")
           .Produces<PagedResponse<List<Conta>?>>()
            .Produces(StatusCodes.Status400BadRequest);

        private static async Task<IResult> HandleAsync(
            IContaHandler handler,
            [FromQuery] int pageNumber = Configuration.DEFAULT_PAGE_NUMBER,
            [FromQuery] int pageSize = Configuration.DEFAULT_PAGE_SIZE
            )
        { 
            var request = new ObterTudoContaRequest()
            {
                pageNumber = pageNumber,
                pageSize = pageSize
            };

            var result = await handler.ObterTudoAsync(request);
            return result.IsSucess ?
                     TypedResults.Ok(result.data) :
                     TypedResults.BadRequest(result);
        }
    }
}
