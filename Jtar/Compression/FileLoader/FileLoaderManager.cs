using System.Collections.Concurrent;

namespace Jtar.Compression.FileLoader;

public class FileLoaderManager
{
    public BlockingCollection<string> Filepaths { get; } = new BlockingCollection<string>(new ConcurrentQueue<string>());

    private readonly LinkedList<Thread> _workerThreads = new LinkedList<Thread>();

    public FileLoaderManager()
    {
        
    }

    public void Run()
    {
        var worker = new FileLoaderWorker(Filepaths);
        var workerThread = new Thread(new ThreadStart(worker.Run));
        workerThread.Start();
    }
}