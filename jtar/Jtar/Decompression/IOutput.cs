namespace Jtar.Decompression;

public interface IOutput<T>
{
    void Output(T obj);
}