namespace Decompressor
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: Decompressor <path to .tar.zstd file>");
                return;
            }

            string filepath = args[0];
            Decompressor decompressor = new Decompressor(filepath);
            decompressor.Decompress();

            Console.WriteLine($"Decompressed {filepath} successfully.");
        }
    }
}