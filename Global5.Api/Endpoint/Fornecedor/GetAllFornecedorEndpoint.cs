using Global5.Api.Common;
using Global5.Core.Handlers;
using Global5.Core.Requests;
using Global5.Core.Responses;

namespace Global5.Api.Endpoint.Fornecedor;

public class GetAllFornecedorEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Fornecedor: Get All")
            .WithSummary("Recupera todos os fornecedores")
            .WithDescription("Recupera todos os fornecedores")
            .WithOrder(4)
            .Produces<Response<List<Core.Models.Fornecedor>?>>();
    
    private static async Task<IResult> HandleAsync(
        IFornecedorHandler handler)
    {
        var request = new GetAllFornecedorRequest()
        {
            UserId = ApiConfiguration.UserId
        };
        var result = await handler.GetAllAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}