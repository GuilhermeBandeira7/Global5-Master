using Global5.Api.Common;
using Global5.Core.Handlers;
using Global5.Core.Requests.EstoqueRequest;
using Global5.Core.Responses;

namespace Global5.Api.Endpoint.Estoque;

public class EditMovimentationEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Estoque: Update")
            .WithSummary("Atualiza estoque")
            .WithDescription("Atualiza estoque")
            .WithOrder(3)
            .Produces<Response<Core.Models.Estoque?>>();
    
    private static async Task<IResult> HandleAsync(
        IEstoqueHandler handler,
        EditMovimentationRequest request,
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