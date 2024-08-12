using Global5.Core;
using Global5.Core.Handlers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Global5.Web;
using Global5.Web.Handlers;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//Using MudBlazor services 
builder.Services.AddMudServices();  

builder.Services.AddHttpClient(WebConfiguration.HttpClientName, ops =>
{
    ops.BaseAddress = new Uri(Configuration.BackendUrl);
});

builder.Services.AddTransient<IMaterialHandler, MaterialHandler>();
builder.Services.AddTransient<IFornecedorHandler, FornecedorHandler>();
builder.Services.AddTransient<IEstoqueHandler, EstoqueHandler>();
builder.Services.AddTransient<IRelatorioHandler, RelatorioHandler>();

await builder.Build().RunAsync();