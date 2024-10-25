
 
namespace DeliveryService;

public class FileLogger : ILogger
{
    private readonly string _filePath;

    public FileLogger(string filePath)
    {
        _filePath = filePath;
    }

    public async Task Log(LogLevel level, string message, Exception? exception = null)
    {
        var logMessage = $"{DateTime.UtcNow}: [{level}] {message}";
        Console.WriteLine($"{DateTime.UtcNow}: [{level}] {message}");
        
        if (exception != null)
        {
            logMessage += $" Exception: {exception.Message}";
        }
        await File.AppendAllTextAsync(_filePath, logMessage + Environment.NewLine);
    }

    public Task LogInfo(string message) => Log(LogLevel.Info, message);
    public Task LogWarning(string message) => Log(LogLevel.Warning, message);
    public Task LogError(string message, Exception? exception = null) => Log(LogLevel.Error, message, exception);
}
