using System.Formats.Tar;

namespace Jtar.Decompression.Output;

public class OutputWorker
{
    private readonly IInput<DecompressionChunk> _input;
    private readonly IOutput<TarEntry> _output;

    public OutputWorker(IInput<DecompressionChunk> input, IOutput<TarEntry> output)
    {
        _input = input;
        _output = output;
    }

    public void Run()
    {
        using var ms = new MemoryStream();
        using var tr = new TarReader(ms);

        DecompressionChunk chunk;
        while (_input.Get(out chunk))
        {
            ms.Write(chunk.Data);
            long oldPosition = ms.Position;
            var entry = tr.GetNextEntry();
            if (entry == null)
            {
                ms.Position = oldPosition;
                continue;
            }
            _output.Output(entry);
        }
    }
}