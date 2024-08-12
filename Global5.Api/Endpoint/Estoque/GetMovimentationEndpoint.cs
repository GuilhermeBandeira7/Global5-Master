using Global5.Api.Common;
using Global5.Core.Handlers;
using Global5.Core.Requests.EstoqueRequest;
using Global5.Core.Responses;

namespace Global5.Api.Endpoint.Estoque;

public class GetMovimentationEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Estoque: Get")
            .WithSummary("Recupera todos as movimentações")
            .WithDescription("Recupera todas as movimentações")
            .WithOrder(4)
            .Produces<Response<List<Core.Models.Estoque>?>>();
    
    private static async Task<IResult> HandleAsync(
        IEstoqueHandler handler)
    {
        var request = new GetMovimentationRequest()
        {
            UserId = ApiConfiguration.UserId
        };
        
        var result = await handler.GetAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}