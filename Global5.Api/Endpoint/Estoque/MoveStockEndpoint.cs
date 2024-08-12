using Global5.Api.Common;
using Global5.Core.Handlers;
using Global5.Core.Requests.EstoqueRequest;
using Global5.Core.Responses;

namespace Global5.Api.Endpoint.Estoque;

public class MoveStockEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Estoque: Move")
            .WithSummary("Movimenta o estoque")
            .WithDescription("Movimenta o estoque")
            .WithOrder(1)
            .Produces<Response<Core.Models.Estoque>>();
    
    private static async Task<IResult> HandleAsync(
        IEstoqueHandler handler,
        MoveStockRequest request)
    {
        request.UserId = ApiConfiguration.UserId;
        var response = await handler.MoveAsync(request);
        return response.IsSuccess
            ? TypedResults.Created($"v1/fornecedores/{response}", response)
            : TypedResults.BadRequest(response);
    }
}