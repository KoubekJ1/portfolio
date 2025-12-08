using Jtar.Compressor;
using Jtar.Decompression.Communication;
using Jtar.Logging;

namespace Jtar.Decompression.ChunkDecompressor;

public class ChunkDecompressorWorker
{
    private readonly IInput<DecompressionChunk> _input;
    private readonly IOutput<DecompressionChunk> _output;
    private readonly ICompressor _compressor;

    public ChunkDecompressorWorker(IInput<DecompressionChunk> input, IOutput<DecompressionChunk> output, ICompressor compressor)
    {
        _input = input;
        _output = output;
        _compressor = compressor;
    }

    public void Run()
    {
        DecompressionChunk? chunk;
        while (_input.Get(out chunk))
        {
            if (chunk == null) continue;
            Logger.Log(LogType.Debug, $"Processing chunk {chunk.Order}");
            var decompressedData = _compressor.Decompress(chunk.Data);
            var outputChunk = new DecompressionChunk(chunk.Order, decompressedData);
            _output.Put(outputChunk);
        }
    }
}