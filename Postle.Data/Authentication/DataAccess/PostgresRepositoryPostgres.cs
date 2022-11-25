using Dapper;
using Postle.Data.Authentication.Models;

namespace Postle.Data.Authentication.DataAccess;

public sealed class PostgresAuthenticationRepository : IAuthenticationRepository
{
    
    public Task<bool> AreCredentialsValid(string email, string password)
    {
        using var db = PostleContext.Connect();
        return Task.FromResult(true);
    }

    public async Task<string> CreateAccount(string name)
    {
        await using var db = PostleContext.Connect();
        var connection = await db.OpenConnectionAsync();
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        var insertedId = await connection.QuerySingleAsync<string>(@"
INSERT INTO ""Account"" (name) 
VALUES (@name)
RETURNING ""Account"".account_id", new { name });

        return insertedId;
    }

    public async Task<string> CreateUser(string email, string password, string firstName, string lastName)
    {
        await using var db = PostleContext.Connect();
        var connection = await db.OpenConnectionAsync();
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        var insertedId = await connection.QuerySingleAsync<string>(@"
INSERT INTO ""User"" (email, password, first_name, last_name) 
VALUES (@email, @password, @firstName, @lastName)
RETURNING ""User"".user_id", 
            new { email, password, firstName, lastName });

        return insertedId;
    }

    public async Task<User?> GetUser(string userId, bool getPassword = false)
    {
        await using var db = PostleContext.Connect();
        var connection = await db.OpenConnectionAsync();
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        var password = getPassword ? "password, " : "";

        var user = await connection.QuerySingleAsync<User>(@$"
SELECT user_id, email, {password} first_name, last_name
FROM ""User"" WHERE user_id = @userId", 
            new { userId });
        
        return user;
    }
    
    public async Task<Account?> GetAccount(string accountId)
    {
        await using var db = PostleContext.Connect();
        var connection = await db.OpenConnectionAsync();
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        var account = await connection.QuerySingleAsync<Account>(@"
SELECT account_id, name
FROM ""Account""
WHERE account_id = @accountId",
            new { accountId });
        
        return account;
    }

    public async Task LinkUserToAccount(string userId, string accountId)
    {
        await using var db = PostleContext.Connect();
        var connection = await db.OpenConnectionAsync();

        await connection.ExecuteAsync($@"
INSERT INTO ""UserAssociations"" (user_id, account_id)
VALUES (@userId, @textId); 
", new { userId, accountId });
    }

    public async Task<List<Account>> GetAccountsLinkedToUser(string userId)
    {
        await using var db = PostleContext.Connect();
        var connection = await db.OpenConnectionAsync();

        var accounts = await connection.QueryAsync<Account>(@"
SELECT a.account_id, a.name FROM ""Account"" a
JOIN ""UserAssociations"" ua on a.account_id = ua.account_id
WHERE a.account_id = @userId
", new { userId });

        return accounts.ToList();
    }
}
