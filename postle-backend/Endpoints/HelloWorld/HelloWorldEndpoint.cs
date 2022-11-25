using Grpc.Core;
using Postle.Web.Endpoints.Authentication.Services;
using Postle.Web.Utils;
using ZicoDev.Protobuf;

namespace Postle.Web.Endpoints.HelloWorld;

public sealed class HelloWorldEndpoint : GreeterService.GreeterServiceBase
{
    private readonly ILogger<HelloWorldEndpoint> _logger;
    private readonly IAuthService _authService;

    public HelloWorldEndpoint(ILogger<HelloWorldEndpoint> logger, IAuthService authService)
    {
        _logger = logger;
    }

    public override Task<SayHelloResponse> SayHello(SayHelloRequest request, ServerCallContext context)
    {
        return ExecuteWithErrorHandling(() => Task.FromResult(new SayHelloResponse()
        {
            Message = $"Hello {request.Name}",
        }));
    }

    private Task<T> ExecuteWithErrorHandling<T>(Func<Task<T>> func)
    {
        return Try.To(func, exception =>
        {
            _logger.Log(LogLevel.Error, exception, "An unhandled exception occured");
        });
    }
}
