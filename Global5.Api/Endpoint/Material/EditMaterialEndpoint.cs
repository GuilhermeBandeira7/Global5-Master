using Global5.Api.Common;
using Global5.Core.Handlers;
using Global5.Core.Requests.MaterialRequest;
using Global5.Core.Responses;

namespace Global5.Api.Endpoint.Material;

public class EditMaterialEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Material: Update")
            .WithSummary("Atualiza um material")
            .WithDescription("Atualiza um material")
            .WithOrder(3)
            .Produces<Response<Core.Models.Material?>>();
    
    private static async Task<IResult> HandleAsync(
        IMaterialHandler handler,
        EditMaterialRequest request,
        int id)
    {
        request.UserId = ApiConfiguration.UserId;
        request.Id = id;

        var result = await handler.EditAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}