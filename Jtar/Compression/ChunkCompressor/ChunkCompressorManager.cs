using System.Collections.Concurrent;

namespace Jtar.Compression.ChunkCompressor;

public class ChunkCompressorManager
{
    public BlockingCollection<Chunk> Chunks { get; private set; }
    private readonly int _workerCount;
    private readonly BlockingCollection<Chunk> _outputCollection = new BlockingCollection<Chunk>(new ConcurrentQueue<Chunk>());

    public ChunkCompressorManager(int workerCount, BlockingCollection<Chunk> outputCollection)
    {
        Chunks = new BlockingCollection<Chunk>(new ConcurrentQueue<Chunk>());
        _workerCount = workerCount;
        _outputCollection = outputCollection;
    }

    public void Run()
    {
        for (int i = 0; i < _workerCount; i++)
        {
            ChunkCompressorWorker worker = new ChunkCompressorWorker(Chunks, _outputCollection);
            Thread thread = new Thread(new ThreadStart(worker.Run));
            thread.Start();
        }
    }
}