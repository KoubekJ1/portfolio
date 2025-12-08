using System.Collections.Concurrent;
using Jtar.Logging;

namespace Jtar.Decompression.Communication;

public class BlockingCollectionIO<T> : IInput<T>, IOutput<T>
{
    BlockingCollection<T> _collection;

    public BlockingCollectionIO(BlockingCollection<T> collection)
    {
        _collection = collection;
    }

    public bool Get(out T? obj)
    {
        return _collection.TryTake(out obj);
    }

    public void Put(T obj)
    {
        Logger.Log(LogType.Debug, $"Added {obj} to queue");
        _collection.Add(obj);
    }
}