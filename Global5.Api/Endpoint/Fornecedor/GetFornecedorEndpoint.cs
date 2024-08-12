using Global5.Api.Common;
using Global5.Core.Handlers;
using Global5.Core.Requests.FornecedorRequest;
using Global5.Core.Responses;

namespace Global5.Api.Endpoint.Fornecedor;

public class GetFornecedorEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Fornecedor: Details")
            .WithSummary("Recupera detalhes de um fornecedor")
            .WithDescription("Recupera detalhes de um fornecedor")
            .WithOrder(5)
            .Produces<Response<Core.Models.Material?>>();
    
    private static async Task<IResult> HandleAsync(
        IFornecedorHandler handler,
        int id)
    {
        var request = new GetFornecedorRequest()
        {
            UserId = ApiConfiguration.UserId,
            Id = id
        };
        
        var result = await handler.GetAsync(request);
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}