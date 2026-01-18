using System.Net;
using System.Net.Sockets;
using System.Text;
using Bank.RequestHandling;
using Bank.Services.Logger;

namespace Bank.Server;

public class TcpServer : IDisposable
{
    private TcpListener listener;
    private CancellationTokenSource tokenSource = new CancellationTokenSource();
    private bool running = false;

    private IRequestHandler requestHandler;
    private ILogger logger;

    public TcpServer(IPAddress address, int port, IRequestHandler handler, ILogger? logger = null)
    {
        listener = new TcpListener(address, port);
        this.requestHandler = handler;
        this.logger = logger != null ? logger : new NoLogger();
    }

    public void Dispose()
    {
        Console.WriteLine("dispose");
        Stop();
    }

    public async Task Listen()
    {
        if (running) throw new InvalidOperationException("Server is already running!");

        listener.Start();

        running = true;

        var cancellationToken = tokenSource.Token;
        while (running)
        {
            try
            {
                TcpClient handler = await listener.AcceptTcpClientAsync(cancellationToken);
                Task.Run(async () =>
                {
                    await ClientLoop(handler);
                    handler.Close();
                });
            }
            catch (OperationCanceledException)
            {
                running = false;
            }
        }

        listener.Stop();
    }

    private async Task ClientLoop(TcpClient handler)
    {
        try
        {
            using var reader = new StreamReader(handler.GetStream());

            bool clientRunning = true;
            var context = new ConnectionContext()
            {
                ServerIP = (handler.Client.LocalEndPoint as IPEndPoint)!
            };
            if (context.ServerIP == null) throw new InvalidOperationException("Server IP couldn't be matched to IPEndPoint!");
            while (clientRunning)
            {
                var data = reader.ReadLine();
                if (data == null)
                {
                    clientRunning = false;
                    break;
                }
                var response = requestHandler.HandleRequest(data, context);
            }
        }
        catch (Exception ex)
        {
            logger.Log(LogType.Debug, ex.Message);
        }
        finally
        {
            logger.Log(LogType.Info, "Client connection ended.");
        }
    }

    public void Stop()
    {
        tokenSource.Cancel();
    }

    public bool Running { get => running; }
}