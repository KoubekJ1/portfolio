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
        long oldPosition = 0;
        while (_input.Get(out chunk))
        {
            Console.WriteLine("a");
            ms.Position = ms.Length;
            ms.Write(chunk.Data);
            ms.Position = oldPosition;
            TarEntry? entry = null;
            try
            {
                entry = tr.GetNextEntry();
            }
            catch (EndOfStreamException e)
            {
                //Console.WriteLine(e);
            }
            if (entry == null)
            {
                ms.Position = oldPosition;
                //Console.WriteLine("entry null");
                continue;
            }
            oldPosition = ms.Position;
            _output.Output(entry);
        }
    }
}

/*using System.Formats.Tar;

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

        DecompressionChunk chunk;
        while (_input.Get(out chunk))
        {
            ms.Write(chunk.Data);
        }

        ms.Position = 0;
        using var tr = new TarReader(ms);
        
        TarEntry? entry;
        while ((entry = tr.GetNextEntry()) != null)
        {
            _output.Output(entry);
        }
    }
}*/