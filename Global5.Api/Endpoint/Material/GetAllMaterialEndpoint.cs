using Global5.Api.Common;
using Global5.Core;
using Global5.Core.Handlers;
using Global5.Core.Requests.MaterialRequest;
using Global5.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Global5.Api.Endpoint.Material;

public class GetAllMaterialEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Materiais: Get All")
            .WithSummary("Recupera todos os materiais")
            .WithDescription("Recupera todos os materiais")
            .WithOrder(4)
            .Produces<Response<List<Core.Models.Material>?>>();
    
    private static async Task<IResult> HandleAsync(
        IMaterialHandler handler)
    {
        var request = new GetAllMaterialRequest()
        {
            UserId = ApiConfiguration.UserId
        };
        var result = await handler.GetAllAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}