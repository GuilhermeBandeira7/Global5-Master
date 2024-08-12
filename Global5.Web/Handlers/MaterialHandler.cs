using System.Net.Http.Json;
using Global5.Core.Handlers;
using Global5.Core.Models;
using Global5.Core.Requests.MaterialRequest;
using Global5.Core.Responses;

namespace Global5.Web.Handlers;

public class MaterialHandler(IHttpClientFactory httpClientFactory) : IMaterialHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(WebConfiguration.HttpClientName);
    public async Task<Response<Material?>> CreateAsync(CreateMaterialRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/materiais", request);
        return await result.Content.ReadFromJsonAsync<Response<Material?>>()
               ?? new Response<Material?>(null, 400, "Falha ao criar material");
    }

    public async Task<Response<Material?>> DeleteAsync(DeleteMaterialRequest request)
    {
        var result = await _client.DeleteAsync($"v1/materiais/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Material?>>()
               ?? new Response<Material?>(null, 400, "Falha ao excluir material");
    }

    public async Task<Response<Material?>> GetAsync(GetMaterialRequest request)
        => await _client.GetFromJsonAsync<Response<Material?>>("v1/materiais")
        ?? new Response<Material?>(null, 400, "Não foi possível obter os materiais");

    public async Task<Response<Material?>> EditAsync(EditMaterialRequest request)
    {
        var result = await _client.PutAsJsonAsync($"v1/materiais/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Material?>>()
               ?? new Response<Material?>(null, 400, "Falha ao atualizar material");
    }

    public async Task<Response<List<Material>?>> GetAllAsync(GetAllMaterialRequest request)
        => await _client.GetFromJsonAsync<Response<List<Material>?>>("v1/materiais")
        ?? new Response<List<Material>?>(null, 400, "Não foi possível obter os materiais");
}