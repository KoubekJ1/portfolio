using System.Threading.Tasks;
using Jtar.Compressor;
using Jtar.Decompression.ChunkDecompressor;
using Jtar.Decompression.ChunkSeparator;
using Jtar.Decompression.Output;
using Jtar.Logging;

namespace Jtar.Decompression;

public class DecompressionContext
{
    private readonly byte[] _compressedData;
    private readonly string _outputDir;
    private readonly int _threadCount;

    public DecompressionContext(byte[] compressedData, string outputDir, int threadCount)
    {
        _compressedData = compressedData;
        _outputDir = outputDir;
        _threadCount = threadCount;
    }

    public async Task Decompress()
    {
        Logger.Log(LogType.Debug, "Beginning unpacking...");

        //Directory.CreateDirectory(_outputDir);

        // Zstd magic string (little-endian): 0xFD2FB528
        byte[] magicString =
        [
            Convert.ToByte("0x28", 16),
            Convert.ToByte("0xB5", 16),
            Convert.ToByte("0x2F", 16),
            Convert.ToByte("0xFD", 16),
        ];
        var chunkSeparatorManager = new ChunkSeparatorManager(_compressedData, magicString);
        var chunkDecompressorManager = new ChunkDecompressorManager(new ZstdCompressor(), chunkSeparatorManager.CompressedChunks, _threadCount);
        var outputManager = new OutputManager(chunkDecompressorManager.DecompressedChunks, Path.GetFullPath(_outputDir));

        Task[] tasks =
        [
            Task.Run(chunkSeparatorManager.Run),
            Task.Run(chunkDecompressorManager.Run),
            Task.Run(outputManager.Run),
        ];

        await Task.WhenAll(tasks);
        Logger.Log(LogType.Debug, "Finished unpacking!");
    }
}