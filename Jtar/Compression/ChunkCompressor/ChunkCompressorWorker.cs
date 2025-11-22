using System.Collections.Concurrent;
using ZstdNet;

namespace Jtar.Compression.ChunkCompressor;

public class ChunkCompressorWorker
{
    private readonly BlockingCollection<Chunk> _chunks;
    private readonly BlockingCollection<Chunk> _outputCollection;
    private readonly Compressor _compressor;

    // TODO: Compression level configuration
    public ChunkCompressorWorker(BlockingCollection<Chunk> chunks, BlockingCollection<Chunk> outputCollection)
    {
        _chunks = chunks;
        _compressor = new Compressor();
        _outputCollection = outputCollection;
    }

    ~ChunkCompressorWorker()
    {
        _compressor.Dispose();
    }

    public void Run()
    {
        while (!_chunks.IsCompleted)
        {
            try
            {
                // TODO: Combine multiple small chunks into one for better compression ratio
                Chunk chunk = _chunks.Take();
                
                var compressedData = _compressor.Wrap(chunk.Data);
                Chunk compressedChunk = new Chunk(chunk.Filepath, chunk.Order, chunk.ChunkCount, compressedData);
                _outputCollection.Add(compressedChunk);
            }
            catch (InvalidOperationException)
            {
                // The collection has been marked as complete for adding and is empty.
                break;
            }
        }
    }
}