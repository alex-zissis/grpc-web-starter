using ZicoDev.Protobuf;

namespace Postle.Web.Endpoints.Authentication.Services;

public interface IAuthService
{
    Task<LoginToAccountReply> LoginToAccount(LoginToAccountRequest loginToAccountRequest);

    Task<LoginWithoutSpecifyingAccountReply> LoginWithoutSpecifyingAccount(
        LoginWithoutSpecifyingAccountRequest loginWithoutSpecifyingAccountRequest);
}
