using Microsoft.AspNetCore.Server.Kestrel.Core;
using Postle.Data.Authentication.DataAccess;
using Postle.Web.Endpoints.Authentication;
using Postle.Web.Endpoints.Authentication.Services;
using Postle.Web.Endpoints.HelloWorld;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.WebHost.ConfigureKestrel(options =>
{
    // Setup a HTTP/2 endpoint without TLS.
    options.ListenAnyIP(5287, o => o.Protocols =
        HttpProtocols.Http2);
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthenticationRepository, PostgresAuthenticationRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<AuthenticationEndpoint>();
app.MapGrpcService<HelloWorldEndpoint>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
