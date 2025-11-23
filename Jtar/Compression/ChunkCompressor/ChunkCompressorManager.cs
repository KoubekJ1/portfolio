using System.Collections.Concurrent;
using Jtar.Compression.Compressor;

namespace Jtar.Compression.ChunkCompressor;

public class ChunkCompressorManager
{
    public BlockingCollection<Chunk> Chunks { get; private set; }
    private readonly int _workerCount;
    private readonly BlockingCollection<Chunk> _outputCollection;
    private readonly ICompressor _compressor;

    public ChunkCompressorManager(int workerCount, ICompressor compressor, BlockingCollection<Chunk> outputCollection)
    {
        Chunks = new BlockingCollection<Chunk>(new ConcurrentQueue<Chunk>());
        _workerCount = workerCount;
        _outputCollection = outputCollection;
        _compressor = compressor;
    }

    public void Run()
    {
        Thread[] threads = new Thread[_workerCount];
        for (int i = 0; i < _workerCount; i++)
        {
            ChunkCompressorWorker worker = new ChunkCompressorWorker(Chunks, (ICompressor)_compressor.Clone(), _outputCollection);
            Thread thread = new Thread(new ThreadStart(worker.Run));
            threads[i] = thread;
            thread.Start();
        }

        Task.Run(() =>
        {
            foreach (var thread in threads)
            {
                thread.Join();
            }
            _outputCollection.CompleteAdding();
        });
    }
}