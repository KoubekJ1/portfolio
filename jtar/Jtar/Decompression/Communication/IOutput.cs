namespace Jtar.Decompression.Communication;

public interface IOutput<T>
{
    void Put(T obj);
}