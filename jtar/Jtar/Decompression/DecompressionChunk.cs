namespace Jtar.Decompression;

public class DecompressionChunk
{
    public int Order { get; private set; }
    public byte[] Data { get; private set; }

    public DecompressionChunk(int order, byte[] data)
    {
        Order = order;
        Data = data;
    }
}