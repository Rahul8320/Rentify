namespace Rentify.Application.Services.Interfaces;

public interface ICachingService
{
    T GetData<T>(string key);
    bool SetData<T>(string key, T value);
    object? RemoveData(string key);
}
