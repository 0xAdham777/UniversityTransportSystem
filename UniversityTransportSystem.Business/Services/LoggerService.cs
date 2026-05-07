using System.Diagnostics;

namespace UniversityTransportSystem.Business.Services;

public static class LoggerService
{
    private static readonly string _logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
    private static readonly object _lock = new();
    
    static LoggerService()
    {
        Directory.CreateDirectory(_logDir);
    }
    
    public enum LogLevel { INFO, WARN, ERROR, FATAL }
    
    public static async Task LogAsync(LogLevel level, string message, Exception? ex = null)
    {
        var filePath = Path.Combine(_logDir, $"app_{DateTime.Now:yyyy-MM-dd}.txt");
        var logLine = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
        if (ex != null) logLine += $" | Exception: {ex.Message}\n{ex.StackTrace}";
        
        await Task.Run(() =>
        {
            lock (_lock)
            {
                File.AppendAllText(filePath, logLine + Environment.NewLine);
            }
        });
        
        Debug.WriteLine(logLine);
    }
    
    public static void Log(LogLevel level, string message, Exception? ex = null)
    {
        Task.Run(async () => await LogAsync(level, message, ex)).ConfigureAwait(false);
    }
}
