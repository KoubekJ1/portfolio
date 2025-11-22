using System.Collections.Concurrent;

namespace Jtar.Compression.ChunkCompressor;

public class ChunkCompressorManager
{
    public BlockingCollection<Chunk> Chunks { get; private set; }
    private readonly int _workerCount;

    public ChunkCompressorManager(int workerCount)
    {
        Chunks = new BlockingCollection<Chunk>(new ConcurrentQueue<Chunk>());
        _workerCount = workerCount;
    }

    public void Run()
    {
        for (int i = 0; i < _workerCount; i++)
        {
            ChunkCompressorWorker worker = new ChunkCompressorWorker(Chunks);
            Thread thread = new Thread(new ThreadStart(worker.Run));
            thread.Start();
        }
    }
}