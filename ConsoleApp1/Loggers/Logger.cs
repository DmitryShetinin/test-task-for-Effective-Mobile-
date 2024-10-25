public enum LogLevel
{
    Info,
    Warning,
    Error
}

public interface ILogger
{
    Task Log(LogLevel level, string message, Exception? exception = null);
    Task LogInfo(string message);
    Task LogWarning(string message);
    Task LogError(string message, Exception? exception = null);
}
