using ZstdSharp;

namespace Decompressor;

public class Decompressor
{
    private readonly string _filepath;

    public Decompressor(string filepath)
    {
        _filepath = filepath;
    }

    public void Decompress()
    {
        using FileStream outputFileStream = new FileStream(_filepath.Replace(".tar.zstd", ".tar"), FileMode.Create);
        using FileStream inputFileStream = new FileStream(_filepath, FileMode.Open);
        using DecompressionStream decompressionStream = new DecompressionStream(inputFileStream);
        decompressionStream.CopyTo(outputFileStream);
    }
}