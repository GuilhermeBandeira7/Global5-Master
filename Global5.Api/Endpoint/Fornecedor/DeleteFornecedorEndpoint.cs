using Global5.Api.Common;
using Global5.Core.Handlers;
using Global5.Core.Requests.FornecedorRequest;
using Global5.Core.Responses;

namespace Global5.Api.Endpoint.Fornecedor;

public class DeleteFornecedorEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Fornecedor: Delete")
            .WithSummary("Deleta um fornecedor")
            .WithDescription("Deleta um fornecedor")
            .WithOrder(2)
            .Produces<Response<Core.Models.Fornecedor>>();
    
    private static async Task<IResult> HandleAsync(
        IFornecedorHandler handler,
        int id)
    {
        var request = new DeleteFornecedorRequest()
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