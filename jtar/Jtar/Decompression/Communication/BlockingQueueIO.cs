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
        Logger.Log(LogType.Debug, $"Thread {Environment.CurrentManagedThreadId} requesting object...");
        if (_collection.IsCompleted)
        {
            Logger.Log(LogType.Debug, $"Thread {Environment.CurrentManagedThreadId} request terminated!");
            obj = default(T);
            return false;
        }
        try
        {
            Logger.Log(LogType.Debug, $"Thread {Environment.CurrentManagedThreadId} request fulfilled!");
            obj = _collection.Take();
            return true;
        }
        catch (InvalidOperationException)
        {
            Logger.Log(LogType.Debug, $"Thread {Environment.CurrentManagedThreadId} request terminated!");
            obj = default(T);
            return false;
        }
    }

    public void Put(T obj)
    {
        Logger.Log(LogType.Debug, $"Added {obj} to queue");
        _collection.Add(obj);
    }
}