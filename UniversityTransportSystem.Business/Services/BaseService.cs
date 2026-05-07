using UniversityTransportSystem.Business.Interfaces;

namespace UniversityTransportSystem.Business.Services;

public class BaseService<T> : IService<T> where T : class
{
    protected readonly IRepository<T> _repository;

    public BaseService(IRepository<T> repository)
    {
        _repository = repository;
    }

    public virtual async Task<List<T>> GetAllAsync()
    {
        try
        {
            return await _repository.GetAllAsync();
        }
        catch (Exception ex)
        {
            LoggerService.Log(LoggerService.LogLevel.ERROR, $"GetAll failed for {typeof(T).Name}", ex);
            throw;
        }
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        try
        {
            return await _repository.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            LoggerService.Log(LoggerService.LogLevel.ERROR, $"GetById failed for {typeof(T).Name} ID={id}", ex);
            throw;
        }
    }

    public virtual async Task<int> InsertAsync(T entity)
    {
        var id = await _repository.InsertAsync(entity);
        LoggerService.Log(LoggerService.LogLevel.INFO, $"Inserted {typeof(T).Name} with ID={id}");
        return id;
    }

    public virtual async Task<bool> UpdateAsync(T entity)
    {
        var result = await _repository.UpdateAsync(entity);
        LoggerService.Log(LoggerService.LogLevel.INFO, $"Updated {typeof(T).Name}, result={result}");
        return result;
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        var result = await _repository.DeleteAsync(id);
        LoggerService.Log(LoggerService.LogLevel.INFO, $"Deleted {typeof(T).Name} ID={id}, result={result}");
        return result;
    }
}
