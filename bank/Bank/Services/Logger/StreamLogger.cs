namespace Bank.Services.Logger;

public class StreamLogger : ILogger
{
    private Stream stream;

    private object logLock = new object();

    public StreamLogger(Stream stream)
    {
        this.stream = stream;
    }

    public void Log(LogType type, string message)
    {
        lock (logLock)
        {
            if (!stream.CanWrite) throw new InvalidOperationException("Unable to write to stream!");
            var prefix = $"[{type.ToString().ToUpper()}]";
            Console.WriteLine($"{prefix} {message}");
        }
    }
}