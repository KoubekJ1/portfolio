using System.Formats.Tar;
using Jtar.Logging;

namespace Jtar.Decompression.Output;

public class TarEntryCreator
{
    public TarEntryCreator()
    {

    }

    private readonly MemoryStream _ms = new();

    public TarEntry? GetTarEntry(byte[] data)
    {
        // Append data
        _ms.Position = _ms.Length;
        _ms.Write(data);

        // Rewind before parsing
        _ms.Position = 0;

        using var tr = new TarReader(_ms, leaveOpen: true);

        TarEntry? entry;

        try
        {
            entry = tr.GetNextEntry();
        }
        catch (EndOfStreamException)
        {
            return null;
        }

        // TarReader advances the position. That's how many bytes were consumed.
        long consumed = _ms.Position;

        Logger.Log(LogType.Debug, $"MS pos after read: {consumed}, length: {_ms.Length}");

        if (entry == null)
            return null;

        // Remaining bytes in stream
        long remaining = _ms.Length - consumed;

        if (remaining > 0)
        {
            // Copy remainder into new buffer
            var buf = new byte[remaining];

            // Read remainder directly
            _ms.Read(buf, 0, (int)remaining);

            // Reset stream
            _ms.SetLength(0);

            // Write back only unread data
            _ms.Write(buf, 0, buf.Length);
        }
        else
        {
            // All data consumed
            _ms.SetLength(0);
        }

        // Reset to start for next iteration
        _ms.Position = 0;

        return entry;
    }
}