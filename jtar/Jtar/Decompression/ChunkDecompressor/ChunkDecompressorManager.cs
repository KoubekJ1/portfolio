using System.Collections.Concurrent;
using Jtar.Compressor;
using Jtar.Decompression.Communication;

namespace Jtar.Decompression.ChunkDecompressor;

public class ChunkDecompressorManager
{
    public BlockingCollectionIO<DecompressionChunk> DecompressedChunks { get; private set; }
    private readonly ICompressor _compressor;
    private readonly BlockingCollection<DecompressionChunk> _outputCollection;
    private readonly IInput<DecompressionChunk> _input;
    private readonly int _threadCount;

    public ChunkDecompressorManager(ICompressor compressor, IInput<DecompressionChunk> input, int threadCount = 1)
    {
        _compressor = compressor;
        _input = input;

        _outputCollection = new BlockingCollection<DecompressionChunk>(new ConcurrentQueue<DecompressionChunk>());
        DecompressedChunks = new BlockingCollectionIO<DecompressionChunk>(_outputCollection);

        _threadCount = threadCount;
    }

    public async Task Run()
    {
        LinkedList<Task> tasks = new LinkedList<Task>();

        for (int i = 0; i < _threadCount; i++)
        {
            var worker = new ChunkDecompressorWorker(_input, DecompressedChunks, _compressor);
            tasks.AddLast(Task.Run(worker.Run));
        }

        await Task.WhenAll(tasks);
        _outputCollection.CompleteAdding();
    }
}