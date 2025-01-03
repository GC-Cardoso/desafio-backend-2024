using Desafio.Api.Common;
using Desafio.Api.Services.Files;
using Desafio.Core.Handler;
using Desafio.Core.Models;
using Desafio.Core.Requests.Conta;
using Desafio.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Api.EndPoints.Contas
{
    public class CriarContaEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Criar Conta")
            .WithDisplayName("Criar Conta")
            .WithSummary("Criação de conta.")
            .WithDescription("Cria os dados de uma conta.")
            .Produces<Response<Conta?>>()
            .Produces(StatusCodes.Status400BadRequest);

        private static async Task<IResult> HandleAsync(
            IContaHandler handler,
            [FromBody]CriarContaRequest request)
        {
            FileService fileService = new FileService();
            var result = await handler.CriarAsync(request, fileService);
            return result.IsSucess ?
                     TypedResults.Created($"{result.data?.contaId}", result.data) :
                     TypedResults.BadRequest(result);
        }
    }
}
