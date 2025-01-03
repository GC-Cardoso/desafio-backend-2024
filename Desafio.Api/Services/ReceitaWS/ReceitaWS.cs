using Desafio.Core.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace Desafio.Api.Services.ReceitaWS
{
    public class ReceitaWS : IReceitaWS
    {
        private readonly HttpClient _client;
        private const string ApiURL = "https://receitaws.com.br/v1/";

        public ReceitaWS()
        {
            _client = new HttpClient()
            {
                Timeout = TimeSpan.FromMinutes(2)
            }; 

            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            
        }

        public async Task<Response<ResponseApi?>> ObterCnpjInfo(string cnpj)
        {
            try
            {
                var request = await _client.GetAsync($"{ApiURL}cnpj/{cnpj}");

                if (request.IsSuccessStatusCode)
                {
                    using var contentStream = await request.Content.ReadAsStreamAsync();
                    var response = await JsonSerializer.DeserializeAsync<ResponseApi>(contentStream);

                    return new Response<ResponseApi?>(response, (int)request.StatusCode, null);
                }

                return new Response<ResponseApi?>(null, (int)request.StatusCode, "Erro ao consultar as informações do CNPJ, por favor verifique os dados e tente novamente em instantes.");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class ResponseApi 
    {
        public string nome { get; set; } = "";

    }

}
