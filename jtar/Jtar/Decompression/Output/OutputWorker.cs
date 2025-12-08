using System.Formats.Tar;
using Jtar.Decompression.Communication;

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
        var creator = new TarEntryCreator();
        var cache = new DataCache();

        DecompressionChunk? chunk;
        while (_input.Get(out chunk))
        {
            if (chunk == null) continue;
            cache.Add(chunk.Order, chunk.Data);
            var sequence = cache.GetUpcomingSequence();
            //Console.WriteLine(sequence.Length + "a");
            if (sequence.Length > 0)
            {
                foreach (var entry in creator.GetTarEntries(sequence))
                {
                    _output.Put(entry);
                }
                //Console.WriteLine(entry == null ? "null" : "entry");
            }
        }
    }
}