using System.Text.Json.Serialization;
using Global5.Api.Handlers;
using Global5.Core;
using Global5.Core.Handlers;

namespace Global5.Api.Common;

public static class BuildExtension
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        ApiConfiguration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
        //Configuration.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
    }
    
    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }
    
    public static void AddCrossOrigin(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(
            options => options.AddPolicy(
                ApiConfiguration.CorsPolicyName,
                policy => policy
                    .WithOrigins([
                        Configuration.BackendUrl,
                        Configuration.FrontendUrl
                    ])
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            ));
    }
    
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => 
                options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        builder
            .Services
            .AddTransient<IMaterialHandler, MaterialHandler>();

        builder
            .Services
            .AddTransient<IFornecedorHandler, FornecedorHandler>();

        builder
            .Services
            .AddTransient<IEstoqueHandler, EstoqueHandler>();

        builder
            .Services
            .AddTransient<IRelatorioHandler, RelatorioHandler>();
    }
}