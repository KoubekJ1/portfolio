using System.Collections.Concurrent;
using Jtar.Compression.Compressor;
using Jtar.Logging;

namespace Jtar.Compression.ChunkCompressor;

public class ChunkCompressorWorker
{
    private readonly BlockingCollection<Chunk> _chunks;
    private readonly BlockingCollection<Chunk> _outputCollection;
    private readonly ICompressor _compressor;

    // TODO: Compression level configuration
    public ChunkCompressorWorker(BlockingCollection<Chunk> chunks, ICompressor compressor, BlockingCollection<Chunk> outputCollection)
    {
        _chunks = chunks;
        _compressor = compressor;
        _outputCollection = outputCollection;
    }

    public void Run()
    {
        while (!_chunks.IsCompleted)
        {
            try
            {
                // TODO: Combine multiple small chunks into one for better compression ratio
                Chunk chunk = _chunks.Take();
                
                var compressedData = _compressor.Compress(chunk.Data);
                Chunk compressedChunk = new Chunk(chunk.Filepath, chunk.Order, chunk.ChunkCount, compressedData);
                _outputCollection.Add(compressedChunk);

                if (_chunks.IsCompleted)
                {
                    _outputCollection.CompleteAdding();
                }
            }
            catch (InvalidOperationException)
            {
                // The collection has been marked as complete for adding and is empty.
                Logger.Log(LogType.Debug, $"ChunkCompressorWorker {Environment.CurrentManagedThreadId} interrupted and finishing");
                _outputCollection.CompleteAdding();
                break;
            }
        }
    }
}