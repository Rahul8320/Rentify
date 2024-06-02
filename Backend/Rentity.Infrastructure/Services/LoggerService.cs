using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Rentify.Domain.Services;
using Serilog;
using Serilog.Events;

namespace Rentity.Infrastructure.Services;

public class LoggerService : ILoggerService
{
    private readonly Serilog.Core.Logger _logger;

    public LoggerService(IConfiguration configuration)
    {
        var loggerConfiguration = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .MinimumLevel.Information();

        _logger = loggerConfiguration.CreateLogger();
    }

    public void LogError(string message, Exception? exception = null)
    {
        message = $"Error :: {message}";
        var logData = new LogData(message, "Error");
        var jsonData = JsonSerializer.Serialize(logData);
        _logger.Error(jsonData, exception);
    }

    public void LogInformation(string message)
    {
        message = $"Information :: {message}";
        var logData = new LogData(message, "Info");
        var jsonData = JsonSerializer.Serialize(logData);
        _logger.Information(jsonData);
    }

    public void LogWarning(string message)
    {
        message = $"Warning :: {message}";
        var logData = new LogData(message, "Warning");
        var jsonData = JsonSerializer.Serialize(logData);
        _logger.Warning(jsonData);
    }
}


public class LogData(string message, string type)
{
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Type { get; set; } = type;
    public string Message { get; set; } = message;
}