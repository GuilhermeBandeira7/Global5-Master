using Global5.Api.Common;
using Global5.Core.Handlers;
using Global5.Core.Requests.RelatorioRequest;
using Global5.Core.Responses;

namespace Global5.Api.Endpoint.Relatorio;

public class GerarRelatorioEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/{fornecedorId}/{materialId}", HandleAsync)
            .WithName("Relatorio: Gerar")
            .WithSummary("Gera relatório")
            .WithDescription("Gera relatório")
            .WithOrder(1)
            .Produces<Response<Core.Models.Relatorio?>>();
    
    private static async Task<IResult> HandleAsync(
        IRelatorioHandler handler,
        GerarRelatorioRequest request,
        int fornecedorId,
        int materialId)
    {
        request.FornecedorId = fornecedorId;
        request.MaterialId = materialId;
        
        var result = await handler.GenerateAsync(request);
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}