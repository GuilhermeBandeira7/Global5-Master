using Global5.Api.Common;
using Global5.Core.Handlers;
using Global5.Core.Requests.FornecedorRequest;
using Global5.Core.Requests.MaterialRequest;
using Global5.Core.Responses;

namespace Global5.Api.Endpoint.Fornecedor;

public class EditFornecedorEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Fornecedor: Update")
            .WithSummary("Atualiza um fornecedor")
            .WithDescription("Atualiza um fornecedor")
            .WithOrder(3)
            .Produces<Response<Core.Models.Fornecedor?>>();
    
    private static async Task<IResult> HandleAsync(
        IFornecedorHandler handler,
        EditFornecedorRequest request,
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