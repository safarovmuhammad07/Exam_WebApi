using Npgsql;

namespace Infrastructure.DataContext;

public class DapperContext
{
    string ConnectionString="Server=localhost; Port=5432;Database=jobboardmanagement;User id=postgres;Password=1234";

    public NpgsqlConnection GetConnection()
    {
        return new NpgsqlConnection(ConnectionString);
    }
}