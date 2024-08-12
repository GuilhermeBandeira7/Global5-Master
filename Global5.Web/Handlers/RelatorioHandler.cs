using System.Net.Http.Json;
using Global5.Core.Handlers;
using Global5.Core.Models;
using Global5.Core.Requests.RelatorioRequest;
using Global5.Core.Responses;

namespace Global5.Web.Handlers;

public class RelatorioHandler(IHttpClientFactory httpClientFactory) : IRelatorioHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(WebConfiguration.HttpClientName);
    public async Task<Response<List<Relatorio>?>> GenerateAsync(GerarRelatorioRequest request)
    {
        var response = await _client.PostAsJsonAsync("v1/relatorio", request);
        return await response.Content.ReadFromJsonAsync<Response<List<Relatorio>?>>()
               ?? new Response<List<Relatorio>?>(null, 400, "Falha ao gerar relatório");
    }
}