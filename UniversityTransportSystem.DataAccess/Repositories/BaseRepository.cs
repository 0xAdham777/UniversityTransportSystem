using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using UniversityTransportSystem.Business.Interfaces;
using UniversityTransportSystem.DataAccess.Infrastructure;

namespace UniversityTransportSystem.DataAccess.Repositories;

public abstract class BaseRepository<T> : IRepository<T> where T : class
{
    private readonly string _tableName;

    protected BaseRepository(string tableName)
    {
        _tableName = tableName;
    }

    public virtual async Task<List<T>> GetAllAsync()
    {
        var sql = $"SELECT * FROM [{_tableName}]";
        var dataTable = await DatabaseHelper.ExecuteQuerySQLAsync(sql);
        return MapDataTableToList(dataTable);
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        var sql = $"SELECT * FROM [{_tableName}] WHERE [{_tableName}ID] = @id";
        var parameters = new[] { new SqlParameter("@id", id) };
        var dataTable = await DatabaseHelper.ExecuteQuerySQLAsync(sql, parameters);

        if (dataTable.Rows.Count == 0)
            return null;

        return MapRowToModel(dataTable.Rows[0]);
    }

    public virtual async Task<int> InsertAsync(T model)
    {
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var idProperty = properties.FirstOrDefault(p =>
            p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
            p.Name.Equals($"{_tableName}ID", StringComparison.OrdinalIgnoreCase));

        var insertProperties = properties
            .Where(p => p != idProperty && p.CanWrite && p.CanRead)
            .ToList();

        var columnNames = string.Join(", ", insertProperties.Select(p => $"[{p.Name}]"));
        var paramNames = string.Join(", ", insertProperties.Select(p => $"@{p.Name}"));

        var sql = $"INSERT INTO [{_tableName}] ({columnNames}) VALUES ({paramNames}); SELECT SCOPE_IDENTITY();";

        var parameters = insertProperties.Select(p =>
            new SqlParameter($"@{p.Name}", p.GetValue(model) ?? DBNull.Value)).ToArray();

        var result = await DatabaseHelper.ExecuteScalarSQLAsync(sql, parameters);
        return Convert.ToInt32(result);
    }

    public virtual async Task<bool> UpdateAsync(T model)
    {
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var idProperty = properties.FirstOrDefault(p =>
            p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
            p.Name.Equals($"{_tableName}ID", StringComparison.OrdinalIgnoreCase));

        if (idProperty is null)
            throw new InvalidOperationException("No ID property found on model.");

        var id = Convert.ToInt32(idProperty.GetValue(model));

        var updateProperties = properties
            .Where(p => p != idProperty && p.CanWrite && p.CanRead)
            .ToList();

        var setClause = string.Join(", ", updateProperties.Select(p => $"[{p.Name}] = @{p.Name}"));

        var sql = $"UPDATE [{_tableName}] SET {setClause} WHERE [{_tableName}ID] = @id";

        var parameters = new List<SqlParameter>
        {
            new SqlParameter("@id", id)
        };
        parameters.AddRange(updateProperties.Select(p =>
            new SqlParameter($"@{p.Name}", p.GetValue(model) ?? DBNull.Value)));

        var rowsAffected = await DatabaseHelper.ExecuteNonQuerySQLAsync(sql, parameters.ToArray());
        return rowsAffected > 0;
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        var sql = $"DELETE FROM [{_tableName}] WHERE [{_tableName}ID] = @id";
        var parameters = new[] { new SqlParameter("@id", id) };
        var rowsAffected = await DatabaseHelper.ExecuteNonQuerySQLAsync(sql, parameters);
        return rowsAffected > 0;
    }

    private static List<T> MapDataTableToList(DataTable dataTable)
    {
        var list = new List<T>(dataTable.Rows.Count);
        foreach (DataRow row in dataTable.Rows)
            list.Add(MapRowToModel(row));
        return list;
    }

    private static T MapRowToModel(DataRow row)
    {
        var model = Activator.CreateInstance<T>();
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in properties)
        {
            if (!property.CanWrite)
                continue;

            var value = row.Table.Columns.Contains(property.Name)
                ? row[property.Name]
                : null;

            if (value is null || value == DBNull.Value)
            {
                property.SetValue(model, null);
                continue;
            }

            var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
            property.SetValue(model, Convert.ChangeType(value, targetType));
        }

        return model;
    }
}
