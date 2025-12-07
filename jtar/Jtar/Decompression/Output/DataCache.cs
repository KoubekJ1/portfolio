namespace Jtar.Decompression.Output;

public class DataCache
{
    private readonly Dictionary<long, byte[]> dataCache = new Dictionary<long, byte[]>();
    private long upcomingIndex = 0;

    public void Add(long index, byte[] data)
    {
        dataCache.Add(index, data);
    }

    public byte[] GetUpcomingSequence()
    {
        using var ms = new MemoryStream();
        while (dataCache.ContainsKey(upcomingIndex))
        {
            var data = dataCache[upcomingIndex];
            ms.Write(data);
            dataCache.Remove(upcomingIndex);
            upcomingIndex++;
        }
        
        return ms.ToArray();
    }
}