using System.Runtime.Caching;
using Rentify.Application.Services.Interfaces;
using Rentify.Domain.Services;

namespace Rentify.Application.Services;

public class CachingService(ILoggerService logger) : ICachingService
{

    /// <summary>
    /// Represents in memroy cache
    /// </summary>
    private readonly ObjectCache _memoryCache = MemoryCache.Default;

    public T GetData<T>(string key)
    {
        try
        {
            T item = (T)_memoryCache.Get(key);
            return item;
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, exception: ex);
            throw;
        }
    }

    public object? RemoveData(string key)
    {
        try
        {
            if (string.IsNullOrEmpty(key))
            {
                logger.LogWarning(message: $"Not found any data for key: {key}");
                return null;
            }

            var result = _memoryCache.Remove(key);
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, exception: ex);
            throw;
        }
    }

    public bool SetData<T>(string key, T value)
    {
        try
        {
            if (string.IsNullOrEmpty(key) || value == null)
            {
                logger.LogWarning(message: $"Key and Value must be not null. Key: {key}, Value: {value}.");
                return false;
            }

            // Add 10 minutes expiry time
            var expirationTime = DateTimeOffset.Now.AddMinutes(10);

            _memoryCache.Set(key, value, expirationTime);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, exception: ex);
            throw;
        }
    }
}
