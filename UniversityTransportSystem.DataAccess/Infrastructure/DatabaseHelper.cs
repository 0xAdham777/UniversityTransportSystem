using System.Data;
using System.Data.SqlClient;

namespace UniversityTransportSystem.DataAccess.Infrastructure;

public static class DatabaseHelper
{
    public static SqlConnection CreateConnection()
    {
        return new SqlConnection(DatabaseConfig.ConnectionString);
    }

    public static async Task<int> ExecuteNonQueryAsync(string spName, SqlParameter[]? parameters = null)
    {
        await using var connection = CreateConnection();
        await using var command = new SqlCommand(spName, connection);
        command.CommandType = CommandType.StoredProcedure;

        if (parameters is not null)
            command.Parameters.AddRange(parameters);

        await connection.OpenAsync();
        return await command.ExecuteNonQueryAsync();
    }

    public static async Task<DataTable> ExecuteQueryAsync(string spName, SqlParameter[]? parameters = null)
    {
        await using var connection = CreateConnection();
        await using var command = new SqlCommand(spName, connection);
        command.CommandType = CommandType.StoredProcedure;

        if (parameters is not null)
            command.Parameters.AddRange(parameters);

        var dataTable = new DataTable();
        using var adapter = new SqlDataAdapter(command);
        adapter.Fill(dataTable);
        return dataTable;
    }

    public static async Task<object?> ExecuteScalarAsync(string spName, SqlParameter[]? parameters = null)
    {
        await using var connection = CreateConnection();
        await using var command = new SqlCommand(spName, connection);
        command.CommandType = CommandType.StoredProcedure;

        if (parameters is not null)
            command.Parameters.AddRange(parameters);

        await connection.OpenAsync();
        return await command.ExecuteScalarAsync();
    }

    public static async Task<int> ExecuteNonQuerySQLAsync(string sql, SqlParameter[]? parameters = null)
    {
        await using var connection = CreateConnection();
        await using var command = new SqlCommand(sql, connection);
        command.CommandType = CommandType.Text;

        if (parameters is not null)
            command.Parameters.AddRange(parameters);

        await connection.OpenAsync();
        return await command.ExecuteNonQueryAsync();
    }

    public static async Task<DataTable> ExecuteQuerySQLAsync(string sql, SqlParameter[]? parameters = null)
    {
        await using var connection = CreateConnection();
        await using var command = new SqlCommand(sql, connection);
        command.CommandType = CommandType.Text;

        if (parameters is not null)
            command.Parameters.AddRange(parameters);

        var dataTable = new DataTable();
        using var adapter = new SqlDataAdapter(command);
        adapter.Fill(dataTable);
        return dataTable;
    }

    public static async Task<object?> ExecuteScalarSQLAsync(string sql, SqlParameter[]? parameters = null)
    {
        await using var connection = CreateConnection();
        await using var command = new SqlCommand(sql, connection);
        command.CommandType = CommandType.Text;

        if (parameters is not null)
            command.Parameters.AddRange(parameters);

        await connection.OpenAsync();
        return await command.ExecuteScalarAsync();
    }
}
