using Global5.Api.Common;
using Global5.Api.Endpoint.Estoque;
using Global5.Api.Endpoint.Fornecedor;
using Global5.Api.Endpoint.Material;
using Global5.Api.Endpoint.Relatorio;
using Global5.Core.Requests.MaterialRequest;

namespace Global5.Api.Endpoint;

public static class Endpoint
{
    public static void MapEndpoint(this WebApplication app)
    {
        var endpoints = app.MapGroup("");
        
        endpoints.MapGroup("/")
            .WithTags("Health Check")
            .MapGet("/", () => new { message = "OK" });

        endpoints.MapGroup("v1/materiais")
            .WithTags("Materiais")
            .MapEndpoint<CreateMaterialEndpoint>()
            .MapEndpoint<EditMaterialEndpoint>()
            .MapEndpoint<DeleteMaterialEndpoint>()
            .MapEndpoint<GetMaterialEndpoint>()
            .MapEndpoint<GetAllMaterialEndpoint>();
        
        endpoints.MapGroup("v1/fornecedores")
            .WithTags("Fornecedores")
            .MapEndpoint<CreateFornecedorEndpoint>()
            .MapEndpoint<EditFornecedorEndpoint>()
            .MapEndpoint<DeleteFornecedorEndpoint>()
            .MapEndpoint<GetFornecedorEndpoint>()
            .MapEndpoint<GetAllFornecedorEndpoint>();

        endpoints.MapGroup("v1/estoque")
            .WithTags("Estoque")
            .MapEndpoint<MoveStockEndpoint>()
            .MapEndpoint<EditMovimentationEndpoint>()
            .MapEndpoint<RemoveMovimentationEndpoint>()
            .MapEndpoint<GetMovimentationEndpoint>();

        endpoints.MapGroup("v1/relatorio")
            .WithTags("Relatorio")
            .MapEndpoint<GerarRelatorioEndpoint>();
    }
    
    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}