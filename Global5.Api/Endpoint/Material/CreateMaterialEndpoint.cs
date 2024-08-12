using Global5.Api.Common;
using Global5.Core.Handlers;
using Global5.Core.Requests.MaterialRequest;
using Global5.Core.Responses;
using Global5.Core.Models;

namespace Global5.Api.Endpoint.Material;
public class CreateMaterialEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Material: Create")
            .WithSummary("Cria um novo material")
            .WithDescription("Cria um novo material")
            .WithOrder(1)
            .Produces<Response<Core.Models.Material>>();
    
    private static async Task<IResult> HandleAsync(
        IMaterialHandler handler,
        CreateMaterialRequest request)
    {
        request.UserId = ApiConfiguration.UserId;
        var response = await handler.CreateAsync(request);
        return response.IsSuccess
            ? TypedResults.Created($"v1/materiais/{response.Data?.Id}", response)
            : TypedResults.BadRequest(response);
    }
}