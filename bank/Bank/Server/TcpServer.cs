using System.Net;
using System.Net.Sockets;
using System.Text;
using Bank.Util.Logging;

namespace Bank.Connection;

public class TcpServer : IDisposable
{
    private TcpListener listener;

    private CancellationTokenSource tokenSource = new CancellationTokenSource();

    private bool running = false;

    public TcpServer(IPAddress address, int port)
    {
        listener = new TcpListener(address, port);
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
            var buffer = new byte[1024];
            while (clientRunning)
            {
                var data = reader.ReadLine();
                if (data == null) clientRunning = false;
                
                
            }
        }
        catch (Exception ex)
        {
            Logger.Log(LogType.Error, "exception");
            Logger.Log(ex);
        }
        finally
        {
            Logger.Log(LogType.Info, "Client connection ended.");
        }
    }

    public void Stop()
    {
        tokenSource.Cancel();
    }

    public bool Running { get => running; }
}