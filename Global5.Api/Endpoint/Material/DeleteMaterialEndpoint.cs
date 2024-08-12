using Global5.Api.Common;
using Global5.Core.Handlers;
using Global5.Core.Requests.MaterialRequest;
using Global5.Core.Responses;

namespace Global5.Api.Endpoint.Material;

public class DeleteMaterialEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Material: Delete")
            .WithSummary("Deleta um material")
            .WithDescription("Deleta um material")
            .WithOrder(2)
            .Produces<Response<Core.Models.Material>>();
    
    private static async Task<IResult> HandleAsync(
        IMaterialHandler handler,
        int id)
    {
        var request = new DeleteMaterialRequest()
        {
            UserId = ApiConfiguration.UserId,
            Id = id
        };

        var result = await handler.DeleteAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}