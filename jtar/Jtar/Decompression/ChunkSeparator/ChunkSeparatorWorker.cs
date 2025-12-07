namespace Jtar.Decompression;

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
        // ! Change int to long !

        // The order of the current chunk
        int order = -1;

        // Index of where the next chunk of raw data starts (excluding the magic string)
        int dataBeginningOffset = -1;

        for (int i = 0; i < _data.Length - _magicString.Length; i++)
        {
            // Check if the following bytes match the magic string
            bool magicStringValid = true;
            for (int j = 0; j < _magicString.Length; j++)
            {
                int index = i+j;
                if (_data[index] != _magicString[j])
                {
                    magicStringValid = false;
                    break;
                }
            }

            if (magicStringValid)
            {
                // First magic string will mark the beginning of the first chunk
                if (order < 1)
                {
                    order = 0;
                    dataBeginningOffset = i + _magicString.Length - 1;
                    continue;
                }

                // Index where the raw data ends
                int dataEnd = i - dataBeginningOffset;

                var segment = new ArraySegment<byte>(_data, dataBeginningOffset, dataEnd);
                
                var chunk = new DecompressionChunk(order, segment.ToArray());
                _output.Output(chunk);
                dataBeginningOffset = i + _magicString.Length - 1;
                order++;
            }
        }
    }
}