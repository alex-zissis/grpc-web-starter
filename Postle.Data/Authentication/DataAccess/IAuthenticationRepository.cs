using Postle.Data.Authentication.Models;

namespace Postle.Data.Authentication.DataAccess;

public interface IAuthenticationRepository
{
    Task<bool> AreCredentialsValid(string email, string password);
    Task<string> CreateUser(string email, string password, string firstName, string lastName);
    Task<string> CreateAccount(string name);
    Task<User?> GetUser(string userId, bool getPassword = false);
    Task<Account?> GetAccount(string accountId);
    Task LinkUserToAccount(string userId, string accountId);
    Task<List<Account>> GetAccountsLinkedToUser(string userId);
}
