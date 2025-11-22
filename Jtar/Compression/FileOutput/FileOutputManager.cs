using System.Collections.Concurrent;
using Jtar.Compression.ChunkCompressor;
using Jtar.Compression.Compressor;

namespace Jtar.Compression.FileOutput;

public class FileOutputManager
{
    public BlockingCollection<Chunk> Chunks { get; private set; }

    private readonly Stream _outputStream;
    private readonly ICompressor _compressor;

    public FileOutputManager(ICompressor compressor, Stream outputStream)
    {
        _outputStream = outputStream;
        _compressor = compressor;
        Chunks = new BlockingCollection<Chunk>(new ConcurrentQueue<Chunk>());
    }

    public void Run()
    {
        FileOutputWorker worker = new FileOutputWorker(Chunks, (ICompressor)_compressor.Clone(), _outputStream);
        Thread thread = new Thread(new ThreadStart(worker.Run));
        thread.Start();
    }
}