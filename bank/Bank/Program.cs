using Bank.Connection;

namespace Bank;

class Program
{
    static async Task Main(string[] args)
    {
        using var server = new TcpServer(new System.Net.IPAddress([0, 0, 0, 0]), 65250);
        await server.Listen();
    }
}
