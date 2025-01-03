using Desafio.Api.Common;
using Desafio.Api.EndPoints.Contas;
using Desafio.Api.EndPoints.Movimentos;
using Desafio.Api.EndPoints.Transacoes;

namespace Desafio.Api.EndPoints
{
    public static class EndPoint
    {
        public static void MapEndPoints(this WebApplication app)
        {
            var endpoints = app.MapGroup("/");

            endpoints.MapGroup("/contas").WithTags("Contas")
                .MapEndPoint<CriarContaEndPoint>()
                .MapEndPoint<AlterarContaEndPoint>()
                .MapEndPoint<ObterContaPorIdEndPoint>()
                .MapEndPoint<ObterContasEndPoint>()
                .MapEndPoint<ObterContaPorAgenciaConta>()
                .MapEndPoint<DeletarContaEndPoint>();

            endpoints.MapGroup("/movimentos").WithTags("Movimentos")
                .MapEndPoint<CriarTransacaoEndPoint>()
                .MapEndPoint<CriarSaqueEndPoint>()
                .MapEndPoint<CriarDepositoEndPoint>()
                .MapEndPoint<DeletarMovimentoEndPoint>()
                .MapEndPoint<ObterMovimentoPorIdEndPoint>()
                .MapEndPoint<ObterMovimentosPorContaEndPoint>();
        }

        private static IEndpointRouteBuilder MapEndPoint<TEndPoint>(this IEndpointRouteBuilder app)
            where TEndPoint : IEndPoint
        {
            TEndPoint.Map(app);
            return app;
        }

    }
}
