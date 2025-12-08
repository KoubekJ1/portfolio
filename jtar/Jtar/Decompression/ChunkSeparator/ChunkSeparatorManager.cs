using System.Collections.Concurrent;
using Jtar.Decompression.Communication;
using Jtar.Logging;

namespace Jtar.Decompression.ChunkSeparator;

public class ChunkSeparatorManager
{
    private readonly byte[] _data;
    private readonly byte[] _magicString;

    private readonly BlockingCollection<DecompressionChunk> _sourceCollection;
    public BlockingCollectionIO<DecompressionChunk> CompressedChunks { get; private set; }

    public ChunkSeparatorManager(byte[] data, byte[] magicString)
    {
        _data = data;
        _magicString = magicString;
        _sourceCollection = new BlockingCollection<DecompressionChunk>(new ConcurrentQueue<DecompressionChunk>());
        CompressedChunks = new BlockingCollectionIO<DecompressionChunk>(_sourceCollection);
    }

    public async Task Run()
    {
        var worker = new ChunkSeparatorWorker(CompressedChunks, _magicString, _data);
        Logger.Log(LogType.Debug, "Starting ChunkSeparatorWorker!");
        await Task.Run(worker.Run);
        Logger.Log(LogType.Debug, "after await!");
        _sourceCollection.CompleteAdding();
    }
}