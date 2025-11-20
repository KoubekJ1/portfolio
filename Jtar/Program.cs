using System;
using System.IO;
using System.Text;
using ZstdNet;

// This creates .tar.zst where each file is stored
// as its own independent Zstd frame.
// Decompressed output is a valid tar archive.

class Program
{
    static void Main()
    {
        string output = "independent.tar.zst";
        string[] files = { "file1.txt", "file2.txt" };

        using var outFile = new FileStream(output, FileMode.Create);

        foreach (string file in files)
        {
            byte[] tarHeader = BuildTarHeader(file);
            byte[] fileBytes = File.ReadAllBytes(file);

            // Build the *raw tar segment* for this file
            using var segment = new MemoryStream();
            segment.Write(tarHeader, 0, tarHeader.Length);
            segment.Write(fileBytes, 0, fileBytes.Length);

            // Pad file to 512-byte TAR block
            long rem = fileBytes.Length % 512;
            if (rem != 0)
            {
                int padding = (int)(512 - rem);
                segment.Write(new byte[padding], 0, padding);
            }

            segment.Position = 0;

            // Now compress this segment as a *fully independent ZSTD frame*
            using var compressor = new Compressor();
            byte[] compressed = compressor.Wrap(segment.ToArray());

            outFile.Write(compressed, 0, compressed.Length);
        }

        // Write FINAL 1024 zero bytes as a final tar footer (also compressed)
        {
            byte[] endBlock = new byte[1024];

            using var compressor = new Compressor();
            byte[] compressedEnd = compressor.Wrap(endBlock);

            outFile.Write(compressedEnd, 0, compressedEnd.Length);
        }
    }


    static byte[] BuildTarHeader(string path)
    {
        byte[] header = new byte[512];

        string name = Path.GetFileName(path);
        long size = new FileInfo(path).Length;
        long mtime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        void WriteOctal(long value, int offset, int length)
        {
            string octal = Convert.ToString(value, 8).PadLeft(length - 1, '0');
            Encoding.ASCII.GetBytes(octal).CopyTo(header, offset);
        }

        Encoding.ASCII.GetBytes(name).CopyTo(header, 0); // file name
        WriteOctal(Convert.ToInt32("644", 8), 100, 8);     // mode
        WriteOctal(0, 108, 8);         // uid
        WriteOctal(0, 116, 8);         // gid
        WriteOctal(size, 124, 12);     // size
        WriteOctal(mtime, 136, 12);    // mtime

        // type flag: '0' = file
        header[156] = (byte)'0';

        // magic "ustar"
        Encoding.ASCII.GetBytes("ustar").CopyTo(header, 257);

        // checksum field: fill with spaces first
        for (int i = 148; i < 156; i++)
            header[i] = 0x20;

        // compute checksum
        int chk = 0;
        foreach (byte b in header)
            chk += b;

        string chkOct = Convert.ToString(chk, 8).PadLeft(6, '0');
        Encoding.ASCII.GetBytes(chkOct).CopyTo(header, 148);
        header[154] = 0;
        header[155] = (byte)' ';

        return header;
    }
}
