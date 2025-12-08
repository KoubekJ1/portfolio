using System.Formats.Tar;
using Jtar.Logging;

namespace Jtar.Decompression.Output;

public class TarEntryCreator
{
    public TarEntryCreator()
    {

    }

    private readonly MemoryStream _ms = new();

    public IEnumerable<TarEntry> GetTarEntries(byte[] data)
    {
        // Save previous length so we can revert the whole append if needed
        long oldLength = _ms.Length;

        // Append incoming data
        _ms.Position = _ms.Length;
        _ms.Write(data);

        // Prepare output list
        List<TarEntry> entries = new();

        // Try to parse multiple entries
        while (true)
        {
            // Start parsing from the beginning of the current buffer
            _ms.Position = 0;

            using var tr = new TarReader(_ms, leaveOpen: true);

            TarEntry? entry;

            try
            {
                entry = tr.GetNextEntry();
            }
            catch (EndOfStreamException)
            {
                // Incomplete data → rollback everything from this append
                _ms.SetLength(oldLength);
                _ms.Position = 0;
                break;
            }

            if (entry == null)
            {
                // No more entries → stop
                // Keep current buffer; no rollback needed
                break;
            }

            // SUCCESS: a full entry was parsed
            entries.Add(entry);

            long consumed = _ms.Position;
            long remaining = _ms.Length - consumed;

            if (remaining > 0)
            {
                // Cut off consumed bytes and leave only the remainder
                var buf = new byte[remaining];
                _ms.Read(buf, 0, (int)remaining);

                _ms.SetLength(0);
                _ms.Write(buf, 0, buf.Length);
            }
            else
            {
                // Everything consumed — clear buffer
                _ms.SetLength(0);
            }

            // After removing consumed bytes, loop again:
            // - If more full entries are present → we’ll parse them
            // - If only a partial chunk remains → the next iteration breaks cleanly
        }

        return entries;
    }
}