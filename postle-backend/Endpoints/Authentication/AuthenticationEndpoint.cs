using Grpc.Core;
using Postle.Web.Endpoints.Authentication.Services;
using Postle.Web.Utils;
using ZicoDev.Protobuf;

namespace Postle.Web.Endpoints.Authentication;

public sealed class AuthenticationEndpoint : AuthenticationService.AuthenticationServiceBase
{
    private readonly ILogger<AuthenticationEndpoint> _logger;
    private readonly IAuthService _authService;

    public AuthenticationEndpoint(ILogger<AuthenticationEndpoint> logger, IAuthService authService)
    {
        _logger = logger;
        _authService = authService;
    }

    public override Task<LoginToAccountReply> LoginToAccount(LoginToAccountRequest request, ServerCallContext context)
    {
        return ExecuteWithErrorHandling(() => _authService.LoginToAccount(request));
    }

    public override Task<LoginWithoutSpecifyingAccountReply> LoginWithoutSpecifyingAccount(
        LoginWithoutSpecifyingAccountRequest request, ServerCallContext context)
    {
        return ExecuteWithErrorHandling(() => _authService.LoginWithoutSpecifyingAccount(request));
    }

    private Task<T> ExecuteWithErrorHandling<T>(Func<Task<T>> func)
    {
        return Try.To(func, exception =>
        {
            _logger.Log(LogLevel.Error, exception, "An unhandled exception occured");
        });
    }
}
