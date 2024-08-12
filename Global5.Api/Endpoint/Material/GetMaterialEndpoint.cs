using Global5.Api.Common;
using Global5.Core.Handlers;
using Global5.Core.Requests.MaterialRequest;
using Global5.Core.Responses;

namespace Global5.Api.Endpoint.Material;

public class GetMaterialEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
    .WithName("Materiais: Detalhe")
        .WithSummary("Recupera detalhes de um material")
        .WithDescription("Recupera detalhes de um material")
        .WithOrder(5)
        .Produces<Response<Core.Models.Material?>>();
    
    private static async Task<IResult> HandleAsync(
        IMaterialHandler handler,
        int id)
    {
        var request = new GetMaterialRequest()
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