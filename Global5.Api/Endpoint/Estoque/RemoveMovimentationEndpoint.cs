using Global5.Api.Common;
using Global5.Core.Handlers;
using Global5.Core.Requests.EstoqueRequest;
using Global5.Core.Responses;

namespace Global5.Api.Endpoint.Estoque;

public class RemoveMovimentationEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Estoque: Delete")
            .WithSummary("Deleta uma movimentação")
            .WithDescription("Deleta uma movimentação")
            .WithOrder(2)
            .Produces<Response<Core.Models.Estoque>>();
    
    private static async Task<IResult> HandleAsync(
        IEstoqueHandler handler,
        int id)
    {
        var request = new RemoveMovimentationRequest()
        {
            UserId = ApiConfiguration.UserId,
            Id = id
        };

        var result = await handler.RemoveAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}