using Global5.Api.Common;
using Global5.Core.Handlers;
using Global5.Core.Responses;
using Global5.Core.Models;
using Global5.Core.Requests.FornecedorRequest;

namespace Global5.Api.Endpoint.Fornecedor;

public class CreateFornecedorEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Fornecedor: Create")
            .WithSummary("Cria um novo fornecedor")
            .WithDescription("Cria um novo fornecedor")
            .WithOrder(1)
            .Produces<Response<Core.Models.Fornecedor>>();
    
    private static async Task<IResult> HandleAsync(
        IFornecedorHandler handler,
        CreateFornecedorRequest request)
    {
        request.UserId = ApiConfiguration.UserId;
        var response = await handler.CreateAsync(request);
        return response.IsSuccess
            ? TypedResults.Created($"v1/fornecedores/{response.Data?.Id}", response)
            : TypedResults.BadRequest(response);
    }
}