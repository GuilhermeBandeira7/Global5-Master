using System.Net.Http.Json;
using Global5.Core.Handlers;
using Global5.Core.Models;
using Global5.Core.Requests.EstoqueRequest;
using Global5.Core.Responses;

namespace Global5.Web.Handlers;

public class EstoqueHandler(IHttpClientFactory httpClientFactory) : IEstoqueHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(WebConfiguration.HttpClientName);
    public async Task<Response<Estoque?>> MoveAsync(MoveStockRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/estoque", request);
        return await result.Content.ReadFromJsonAsync<Response<Estoque?>>()
               ?? new Response<Estoque?>(null, 400, "Falha ao criar movimentação");
    }

    public async Task<Response<Estoque?>> RemoveAsync(RemoveMovimentationRequest request)
    {
        var result = await _client.DeleteAsync($"v1/estoque/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Estoque?>>()
               ?? new Response<Estoque?>(null, 400, "Falha excluir movimentação");
    }

    public async Task<Response<Estoque?>> EditAsync(EditMovimentationRequest request)
    {
        var result = await _client.PutAsJsonAsync($"v1/estoque/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Estoque?>>()
               ?? new Response<Estoque?>(null, 400, "Falha editar movimentação");
    }

    public async Task<Response<List<Estoque>?>> GetAsync(GetMovimentationRequest request)
        => await _client.GetFromJsonAsync<Response<List<Estoque>?>>("v1/estoque")
           ?? new Response<List<Estoque>?>(null, 400, "Não foi possível obter as movimentações");
}