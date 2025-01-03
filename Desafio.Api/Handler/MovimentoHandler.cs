using Desafio.Api.Data;
using Desafio.Core.Enums;
using Desafio.Core.Handler;
using Desafio.Core.Models;
using Desafio.Core.Requests.Movimento;
using Desafio.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Api.Handler
{
    public class MovimentoHandler(AppDbContext context) : IMovimentoHandler
    {

        public async Task<Response<Movimento?>> CriarTransacaoAsync(CriarTransacaoRequest request)
        {
            try
            {
                var conta = await context.Contas.FirstOrDefaultAsync(x=> x.contaId == request.contaId);

                if (conta is null)
                {
                    return new Response<Movimento?>(null, 500, "Conta inexistente! Por favor, verifique os dados.");
                }
                else if ( conta.saldo < request.valor)
                {
                    return new Response<Movimento?>(null, 500, "Conta com saldo inferior ao valor de transferencia! Por favor, verifique os dados.");
                }

                var contaAlvo = await context.Contas.FirstOrDefaultAsync(
                    x => x.agencia == request.numeroAgenciaBeneficiario
                     && x.numeroConta == request.numeroContaBeneficiario);

                if (contaAlvo is null)
                {
                    return new Response<Movimento?>(null, 500, "Os dados informados não pertencem a nenhuma conta! Por favor, verifique os dados.");
                }

                Movimento transacao= new Movimento
                {
                    contaId = request.contaId,
                    valor = request.valor,
                    tipoMovimento = ETipoMovimento.Transferencia,
                    contaAlvoId = contaAlvo.contaId,
                    dataCriacao = DateTime.Now
                    
                };

                conta.saldo -= transacao.valor;
                contaAlvo.saldo += transacao.valor;

                context.Contas.Update(conta);
                context.Contas.Update(contaAlvo);
                await context.Movimentos.AddAsync(transacao);

                await context.SaveChangesAsync();

                return new Response<Movimento?>(transacao, 201, "Trasação criada com sucesso");

            }
            catch (Exception)
            {
                return new Response<Movimento?>(null, 500, "Erro ao efetuar transação.");
            }
        }

        public async Task<Response<Movimento?>> CriarSaqueAsync(CriarSaqueRequest request)
        {
            try
            {
                var conta = await context.Contas.FirstOrDefaultAsync(x => x.contaId == request.contaId);

                if (conta is null)
                {
                    return new Response<Movimento?>(null, 500, "Conta inexistente! Por favor, verifique os dados.");
                }
                else if (conta.saldo < request.valor)
                {
                return new Response<Movimento?>(null, 500, "Conta com saldo inferior ao valor de saque! Por favor, verifique os dados.");
                }

            Movimento deposito = new Movimento
                {
                    tipoMovimento = ETipoMovimento.Saque,
                    contaId = request.contaId,
                    valor = request.valor,
                    dataCriacao = DateTime.Now
                };

                conta.saldo -= request.valor;

                await context.Movimentos.AddAsync(deposito);
                await context.SaveChangesAsync();

                return new Response<Movimento?>(deposito, 201, "Saque criado com sucesso");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Response<Movimento?>> CriarDepositoAsync(DepositoRequest request)
        {
            try
            {
                var conta = await context.Contas.FirstOrDefaultAsync(x => x.numeroConta == request.conta && x.numeroConta == request.conta);

                if (conta is null)
                    return new Response<Movimento?>(null,404,"conta not found");

                conta.saldo += request.valor;

                Movimento deposito = new Movimento
                {
                    contaId = conta.contaId,
                    dataCriacao = DateTime.Now,
                    tipoMovimento = ETipoMovimento.Deposito,
                    valor = request.valor,
                };

                await context.Movimentos.AddAsync(deposito);
                await context.SaveChangesAsync();

                return new Response<Movimento?>(deposito, 201, "criado com sucesso");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Response<Movimento?>> DeletarMovimentoAsync(DeletarMovimentoRequest request)
        {
            try
            {

                var conta = await context.Contas.FirstOrDefaultAsync(
                    x => x.contaId == request.contaId);
                if (conta is null)
                    return new Response<Movimento?>(null, 404, "Conta não encontrada, verifique os dados");

                var movimento = await context.Movimentos.FirstOrDefaultAsync(
                    x => x.contaId == request.contaId
                      && x.movimentoId == request.movimentoId);
                if (movimento is null)
                    return new Response<Movimento?>(null, 404, "Movimento não encontrado, verifique os dados");
                
                if(movimento.tipoMovimento == ETipoMovimento.Transferencia)
                {
                    var contaAlvo = await context.Contas.FirstOrDefaultAsync(
                        x => x.contaId == movimento.contaAlvoId);
                    if (contaAlvo is null)
                    return new Response<Movimento?>(null, 404, "Conta Beneficiada não encontrada, não foi possivel excluir a transação.");

                    return await DeletaTransacaoAsync(conta, movimento, contaAlvo) 
                    ? new Response<Movimento?>(movimento, 200, "Transação excluida com sucesso!")
                    : new Response<Movimento?>(null, 500, "Erro ao excluir transação");   
                }
                else if (movimento.tipoMovimento == ETipoMovimento.Deposito)
                {
                    return await DeletaDepositoAsync(conta, movimento)
                    ? new Response<Movimento?>(movimento, 200, "Deposito excluido com sucesso!")
                    : new Response<Movimento?>(null, 500, "Erro ao excluir deposito");
                }
                else if (movimento.tipoMovimento == ETipoMovimento.Saque)
                {
                    return await DeletaSaqueAsync(conta, movimento)
                    ? new Response<Movimento?>(movimento, 200, "Deposito excluido com sucesso!")
                    : new Response<Movimento?>(null, 500, "Erro ao excluir Saque");
                }
                else { return new Response<Movimento?>(null,500,"Erro ao tentar identificar o tipo de movimento.");}
            }
            catch (Exception)
            {
             return new Response<Movimento?>(null, 500, "Erro ao tentar identificar o tipo de movimento.");         }
            }

        public async Task<Response<Movimento?>> ObterMovimentoPorIdAsync(ObterMovimentoPorIdRequest request)
        {
            try
            {
                var conta = await context.Contas.FirstOrDefaultAsync(x => x.contaId == request.contaId);
                if (conta is null)
                    return new Response<Movimento?>(null, 404, "Conta não encontrada.");

                var movimento = await context.Movimentos.FirstOrDefaultAsync(
                    x=> x.contaId==request.contaId
                     && x.movimentoId == request.movimentoId);

                return new Response<Movimento?>(movimento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<PagedResponse<List<Extrato>?>> ObterMovimentosPorContaAsync(ObterMovimentosPorContaRequest request)
        {
            try
            {
                var contaExiste = await context.Contas.FirstOrDefaultAsync(x => x.contaId == request.contaId);
                if (contaExiste is null)
                    return new PagedResponse<List<Extrato>?>(null, 404, "Erro ao buscar dados da conta.");
               
                var query = from movimento in context.Movimentos
                            join conta in context.Contas on movimento.contaId equals conta.contaId into contaJoin
                            from conta in contaJoin.DefaultIfEmpty()
                            join contaAlvo in context.Contas on movimento.contaAlvoId equals contaAlvo.contaId into contaAlvoJoin
                            from contaAlvo in contaAlvoJoin.DefaultIfEmpty()
                            where movimento.contaId == request.contaId || movimento.contaAlvoId == request.contaId
                            orderby movimento.dataCriacao
                            select new
                            {
                                movimento.movimentoId,
                                movimento.contaId,
                                movimento.contaAlvoId,
                                movimento.dataCriacao,
                                movimento.tipoMovimento,
                                nome = movimento.tipoMovimento != 0 ? null : (movimento.contaAlvoId == request.contaId ? conta.nome : contaAlvo.nome),
                                movimento.valor
                            };

                var count = await query.CountAsync();

                List<Extrato> listaExtrato = new List<Extrato>();


                    await query
                    .Skip(request.pageSize * (request.pageNumber - 1))
                    .Take(request.pageSize)
                    .OrderBy(x => x.dataCriacao)
                    .ForEachAsync(movimento =>
                    {
                    decimal valor =0;
                    string descricaoMovimento = string.Empty;
                    switch (movimento.tipoMovimento)
                    {
                        case ETipoMovimento.Transferencia:
                          valor = movimento.contaId == request.contaId ?
                               -movimento.valor :
                                movimento.valor ;
                             descricaoMovimento = movimento.contaId == request.contaId ?
                               "Transação - Saida" :
                               "Transação - Entrada";

                            break;

                        case ETipoMovimento.Deposito:
                            valor = movimento.valor;
                            descricaoMovimento = "Deposito";
                            break;

                        case ETipoMovimento.Saque:
                            valor = -movimento.valor;
                            descricaoMovimento = "Saque";
                            break;

                        default:
                            break;
                    }

                     Extrato extrato = new Extrato()
                    {
                        dataCriacao = movimento.dataCriacao,
                        movimentoId = movimento.movimentoId,
                        participante = movimento.nome,
                        valor = Math.Round(valor, 2),
                        descricaoMovimento = descricaoMovimento,
                    };

                    listaExtrato.Add(extrato);
                    
                });
                    

                return new PagedResponse<List<Extrato>?>(
                    listaExtrato,
                    count,
                    request.pageNumber,
                    request.pageSize
                    );
            }
            catch (Exception)
            {
                return new PagedResponse<List<Extrato>?>(
                   null,
                   500,
                   "Erro ao consultar movimentações"
                   );
            }
        }

        private async Task<bool> DeletaTransacaoAsync(Conta conta, Movimento transacao, Conta contaAlvo)
        {
            try
            {
                conta.saldo += transacao.valor;
                contaAlvo.saldo -= transacao.valor;
                context.Contas.Update(conta);
                context.Contas.Update(contaAlvo);
                context.Movimentos.Remove(transacao);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        private async Task<bool> DeletaDepositoAsync(Conta conta, Movimento deposito)
        {
            try
            {
                conta.saldo -= deposito.valor;
                context.Contas.Update(conta);
                context.Movimentos.Remove(deposito);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        private async Task<bool> DeletaSaqueAsync(Conta conta, Movimento saque)
        {
            try
            {
                conta.saldo += saque.valor;
                context.Contas.Update(conta);
                context.Movimentos.Remove(saque);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
