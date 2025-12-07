namespace Jtar.Decompression;

public interface IInput<T>
{
    bool Get(out T obj);
}