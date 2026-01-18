namespace Bank.Services.Logger;

public interface ILogger
{
    public void Log(LogType type, string message);
}