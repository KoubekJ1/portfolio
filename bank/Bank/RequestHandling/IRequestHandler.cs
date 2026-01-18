using Bank.Server;

namespace Bank.RequestHandling;

public interface IRequestHandler
{
    string HandleRequest(string request, ConnectionContext context);
}