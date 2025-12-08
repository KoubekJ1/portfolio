using System.Formats.Tar;
using System.Text;

namespace Jtar.Compression.FileLoader;

/// <summary>
/// Formats files into PAX tar format.
/// </summary>
public class PaxTarFormatter : ITarFormatter
{
    public PaxTarFormatter()
    {
    }

    /// <summary>
    /// Formats a file into PAX tar format.
    /// </summary>
    /// <param name="path">Input filepath</param>
    /// <param name="rootDir">Root directory used to compute relative paths</param>
    public byte[] FormatTar(string path, string rootDir)
    {
        using var ms = new MemoryStream();
        var writer = new TarWriter(ms, TarEntryFormat.Pax, leaveOpen: true);
        using (var fileStream = File.OpenRead(path))
        {
            string relative = Path.GetRelativePath(rootDir, path)
                                  .Replace("\\", "/");
            var fileInfo = new FileInfo(path);

            var entry = new PaxTarEntry(TarEntryType.RegularFile, relative)
            {
                DataStream = fileStream,
                ModificationTime = fileInfo.LastWriteTimeUtc
            };

            writer.WriteEntry(entry);
        }

        return ms.ToArray();
    }
}