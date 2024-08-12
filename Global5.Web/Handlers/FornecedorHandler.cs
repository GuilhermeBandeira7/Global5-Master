using System.Net.Http.Json;
using Global5.Core.Handlers;
using Global5.Core.Models;
using Global5.Core.Requests;
using Global5.Core.Requests.FornecedorRequest;
using Global5.Core.Responses;

namespace Global5.Web.Handlers;

public class FornecedorHandler(IHttpClientFactory httpClientFactory) : IFornecedorHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(WebConfiguration.HttpClientName);
    public async Task<Response<Fornecedor?>> CreateAsync(CreateFornecedorRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/fornecedores", request);
        return await result.Content.ReadFromJsonAsync<Response<Fornecedor?>>()
               ?? new Response<Fornecedor?>(null, 400, "Falha ao criar fornecedor");
    }

    public async Task<Response<Fornecedor?>> GetAsync(GetFornecedorRequest request)
        => await _client.GetFromJsonAsync<Response<Fornecedor?>>("v1/fornecedores")
           ?? new Response<Fornecedor?>(null, 400, "Não foi possível obter os fornecedores");

    public async Task<Response<Fornecedor?>> DeleteAsync(DeleteFornecedorRequest request)
    {
        var result = await _client.DeleteAsync($"v1/fornecedores/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Fornecedor?>>()
               ?? new Response<Fornecedor?>(null, 400, "Falha ao excluir fornecedor");
    }

    public async Task<Response<Fornecedor?>> EditAsync(EditFornecedorRequest request)
    {
        var result = await _client.PutAsJsonAsync($"v1/fornecedores/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Fornecedor?>>()
               ?? new Response<Fornecedor?>(null, 400, "Falha ao fornecedor material");
    }

    public async Task<Response<List<Fornecedor>?>> GetAllAsync(GetAllFornecedorRequest request)
        => await _client.GetFromJsonAsync<Response<List<Fornecedor>?>>("v1/fornecedores")
           ?? new Response<List<Fornecedor>?>(null, 400, "Não foi possível obter os fornecedores");
}