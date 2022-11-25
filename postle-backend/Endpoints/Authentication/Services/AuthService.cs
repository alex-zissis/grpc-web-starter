using Postle.Data.Authentication.DataAccess;
using ZicoDev.Protobuf;

namespace Postle.Web.Endpoints.Authentication.Services;

public sealed class AuthService : IAuthService
{
    private readonly IAuthenticationRepository _authenticationRepository;

    public AuthService(IAuthenticationRepository authenticationRepository)
    {
        _authenticationRepository = authenticationRepository;
    }

    public Task<LoginToAccountReply> LoginToAccount(LoginToAccountRequest loginToAccountRequest)
    {
        throw new NotImplementedException();
    }

    public Task<LoginWithoutSpecifyingAccountReply> LoginWithoutSpecifyingAccount(
        LoginWithoutSpecifyingAccountRequest loginWithoutSpecifyingAccountReply)
    {
        throw new NotImplementedException();
    }
}
