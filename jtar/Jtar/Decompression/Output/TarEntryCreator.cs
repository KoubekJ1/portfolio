using System.Formats.Tar;

namespace Jtar.Decompression.Output;

public class TarEntryCreator
{
    public TarEntryCreator()
    {

    }

    private readonly MemoryStream _ms = new();

    public TarEntry? GetTarEntry(byte[] data)
    {
        _ms.Position = _ms.Length;
        _ms.Write(data);
        _ms.Position = 0;
        
        using var tr = new TarReader(_ms, leaveOpen: true);

        try
        {
            var entry = tr.GetNextEntry();
            _ms.Position = 0;

            if (entry != null)
            {
                byte[] buffer = new byte[_ms.Length - _ms.Position];
                _ms.Read(buffer, (int)_ms.Position, buffer.Length);
                _ms.SetLength(0);
                _ms.Write(buffer);
            }

            return entry;
        }
        catch (EndOfStreamException)
        {
            return null;
        }
    }
}