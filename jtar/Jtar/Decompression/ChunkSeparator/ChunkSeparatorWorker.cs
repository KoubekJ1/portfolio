using Jtar.Decompression.Communication;
using Jtar.Logging;

namespace Jtar.Decompression.ChunkSeparator;

public class ChunkSeparatorWorker
{
    private readonly byte[] _data;
    private readonly byte[] _magicString;
    private readonly IOutput<DecompressionChunk> _output;

    public ChunkSeparatorWorker(IOutput<DecompressionChunk> output, byte[] magicString, byte[] data)
    {
        _output = output;
        _magicString = magicString;
        _data = data;
    }

    public void Run()
    {
        Logger.Log(LogType.Debug, "ChunkSeparatorWorker started!");

        int order = -1;

        long dataBegin = -1;
        long dataLength = _data.LongLength;
        long magicLength = _magicString.LongLength;

        for (long i = 0; i <= dataLength - magicLength; i++)
        {
            bool match = true;
            for (long j = 0; j < magicLength; j++)
            {
                if (_data[i + j] != _magicString[j])
                {
                    match = false;
                    break;
                }
            }

            if (!match)
                continue;

            Logger.Log(LogType.Debug, "Found magic string!");

            // First magic string -> start first chunk including the magic string
            if (order < 0)
            {
                order = 0;
                dataBegin = i;
                i += (magicLength - 1);
                continue;
            }

            // Emit previous chunk from dataBegin to (i - 1)
            long chunkLength = i - dataBegin;
            if (chunkLength > 0)
            {
                var segment = new ArraySegment<byte>(_data, (int)dataBegin, (int)chunkLength);
                var chunk = new DecompressionChunk(order, segment.ToArray());
                _output.Put(chunk);
            }

            // Next chunk starts INCLUDING this magic string
            order++;
            dataBegin = i;

            // Skip ahead
            i += (magicLength - 1);
        }

        // Emit final chunk (if any)
        if (order >= 0 && dataBegin < dataLength)
        {
            long finalLength = dataLength - dataBegin;
            if (finalLength > 0)
            {
                var segment = new ArraySegment<byte>(_data, (int)dataBegin, (int)finalLength);
                var finalChunk = new DecompressionChunk(order, segment.ToArray());
                _output.Put(finalChunk);
            }
        }
    }

}