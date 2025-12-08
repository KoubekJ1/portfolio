namespace Jtar.Decompression.Communication;

public interface IInput<T>
{
    bool Get(out T? obj);
}