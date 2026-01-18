using Bank.Server;
using Bank.Services.Logger;

namespace Bank.RequestHandling;

public class RequestHandler : IRequestHandler
{
    private ILogger logger;

    public RequestHandler(ILogger? logger = null)
    {
        this.logger = logger != null ? logger : new NoLogger();
    }

    public string HandleRequest(string request, ConnectionContext connectionContext)
    {
        logger.Log(LogType.Info, request);
        return "Jek";
    }
}