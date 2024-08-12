using Global5.Api;
using Global5.Api.Common;
using Global5.Api.Endpoint;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseCors(ApiConfiguration.CorsPolicyName);
app.MapEndpoint();

app.Run();