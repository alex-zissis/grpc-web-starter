using Npgsql;

namespace Postle.Data;

public class PostleContext
{
    private const string ConnectionString = "Host=localhost;Username=postle;Password=postle;Database=postle";

    public static NpgsqlDataSource Connect()
    {
        return NpgsqlDataSource.Create(ConnectionString);
    }
}