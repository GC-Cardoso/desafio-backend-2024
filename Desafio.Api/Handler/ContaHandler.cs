using Desafio.Api.Data;
using Desafio.Api.Services.Files;
using Desafio.Api.Services.ReceitaWS;
using Desafio.Core.Handler;
using Desafio.Core.Models;
using Desafio.Core.Requests.Conta;
using Desafio.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Api.Handler
{
    public class ContaHandler (AppDbContext context , IReceitaWS receitaWS) : IContaHandler
    {
    private readonly IReceitaWS _receitaWS = receitaWS;


        public async Task<Response<Conta?>> AlterarAsync(AlterarContaRequest request)
        {
            try
            {

                var conta = await context.Contas.FirstOrDefaultAsync(x => x.contaId == request.contaId);

                if (conta is null)
                    return new Response<Conta?>(null, StatusCodes.Status404NotFound, "Conta não encontrada.");

                var dados = await _receitaWS.ObterCnpjInfo(request.cnpj);

                if (!dados.IsSucess || dados.data is null)
                {
                    return new Response<Conta?>(null, StatusCodes.Status404NotFound, dados.message);
                }

                FileService fileService = new FileService();
                var upload = await fileService.UploadAsync(request.imagemDocumento);

                if (upload is null || upload.Equals("Imagem invalida"))
                    return new Response<Conta?>(null, StatusCodes.Status400BadRequest, "A imagem enviada não é do tipo base64.");

                fileService.Remove(conta.imagemDocumento);

                conta.nome = dados.data.nome;
                conta.cnpj = request.cnpj;
                conta.imagemDocumento = upload;
                
                context.Contas.Update(conta);
                await context.SaveChangesAsync();

                return new Response<Conta?>(conta);
            }
            catch (Exception)
            {

                return new Response<Conta?>(null, StatusCodes.Status500InternalServerError, "Erro ao alterar conta.");
            }
        }

        public async Task<Response<Conta?>> CriarAsync(CriarContaRequest request, FileService fileService)
        {
            try
            {
                if (!ValidarCnpj(request.cnpj))
                    return new Response<Conta?>(null, StatusCodes.Status400BadRequest , message: "CNPJ invalido.");

                var dados = await _receitaWS.ObterCnpjInfo(request.cnpj);

                if (!dados.IsSucess || dados.data is null)
                {
                    return new Response<Conta?>(null, 404, dados.message);
                }

                Random random = new Random();

                var img = await fileService.UploadAsync(request.imagemDocumento);
                if (img is null || img.Equals("Imagem invalida"))
                    return new Response<Conta?>(null, 500, "A imagem enviada não é do tipo base64.");

                Conta novaConta = new Conta()
                {
                    nome = dados.data.nome,
                    cnpj = request.cnpj,
                    numeroConta = random.Next(100000, 999999).ToString(),
                    agencia = random.Next(1000, 9999).ToString(),
                    imagemDocumento = img,
                    saldo = 0m
                };

                await context.Contas.AddAsync(novaConta);
                await context.SaveChangesAsync();

                return new Response<Conta?>(novaConta);
            }
            catch (Exception)
            {
                return new Response<Conta?>(null, 500, "Erro ao criar Conta.");
            }
        }

        public async Task<Response<Conta?>> DeletarAsync(DeletarContaRequest request)
        {
            try
            {

                var conta = await context.Contas.FirstOrDefaultAsync(x => x.contaId == request.contaId);

                if (conta is null)
                    return new Response<Conta?>(null, 404, "Conta não encontrada.");


                FileService fileService = new FileService();
                fileService.Remove(conta.imagemDocumento);
                context.Contas.Remove(conta);
                await context.SaveChangesAsync();

                return new Response<Conta?>(conta);
            }
            catch (Exception)
            {

                return new Response<Conta?>(null, 500, "Erro ao excluir conta.");
            }
        }

        public async Task<Response<Conta?>> ObterPorAgenciaContaAsync(ObterContaPorAgenciaContaRequest request)
        {
            try
            {
                var conta = await context.Contas.FirstOrDefaultAsync(
                    x=> x.numeroConta == request.numeroConta
                     && x.agencia == request.numeroAgencia );

                return conta is null ?
                     new Response<Conta?>(null, 404, "Conta não encontrada.") :
                     new Response<Conta?>(conta);
            }
            catch (Exception)
            {

                return new Response<Conta?>(null, 500, "Erro ao buscar conta.");
            }
        }

        public async Task<Response<Conta?>> ObterPorIdAsync(ObterContaRequest request)
        {
            try
            {

                var conta = await context.Contas.FirstOrDefaultAsync(x => x.contaId == request.contaId);

                return conta is null?
                     new Response<Conta?>(null, 404, "Conta não encontrada."):
                     new Response<Conta?>(conta);
            }
            catch (Exception)
            {

                return new Response<Conta?>(null, 500, "Erro ao buscar conta.");
            }
        }

        public async Task<PagedResponse<List<Conta>?>> ObterTudoAsync(ObterTudoContaRequest request)
        {
            try
            {
                var querry = context.Contas.AsNoTracking();
                 
                var contas = await querry
                    .Skip(request.pageSize * (request.pageNumber - 1))
                    .Take(request.pageSize)
                    .OrderBy(x => x.contaId)
                    .ToListAsync();

                var contador = await querry.CountAsync();

                return new PagedResponse<List<Conta>?>(
                    contas,
                    contador,
                    request.pageNumber,
                    request.pageSize
                    );
                
            }
            catch (Exception)
            {
                return new PagedResponse<List<Conta>?>(
                    null,
                    500,
                    "Erro ao consultar contas"
                    );
            }
        }

        private bool ValidarCnpj(string cnpjString)
        {
            try
            {

            var cnpj = cnpjString.ToList().Select(x => (int)Char.GetNumericValue(x)).ToList();

            List<int> listaValidadora = new List<int>{ 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var Somatorio = 0;

            for (int i = 0; i <= listaValidadora.Count-1; i++)
            {
                Somatorio += (cnpj[i]) * listaValidadora[i];
            }
            Somatorio = Somatorio % 11;
            var primeiroDigito = Somatorio > 2 ?  11 - Somatorio : 0;

            if(cnpj[12] != primeiroDigito) 
                return false;

            listaValidadora.Insert( 0, 6 );
            cnpj.Insert(12,primeiroDigito);

            Somatorio = 0;

            for (int i = 0; i <= listaValidadora.Count-1; i++)
            {
                Somatorio += cnpj[i] * listaValidadora[i];
            }
            Somatorio = Somatorio % 11;

            var segundoDigito = Somatorio >= 2 ? 11 - Somatorio : 0;

            return cnpj[14] == segundoDigito;
            }

            catch (Exception)
            {

                return false;
            }

        }
    }
}
