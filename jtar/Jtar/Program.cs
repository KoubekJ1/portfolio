using System.CommandLine;
using Jtar.CommandActions;

namespace Jtar;

class Program
{
    static int Main(string[] args)
    {
        var rootCommand = new RootCommand("jtar");

        var compressCommand = new Command("compress", "Compresses the specified files");
        var compressionAction = new CompressionAction();
        compressionAction.AddToCommand(compressCommand);
        compressCommand.Action = compressionAction;

        rootCommand.Subcommands.Add(compressCommand);

        var decompressCommand = new Command("decompress", "Decompress the specified archive");
        var decompressionAction = new DecompressionAction();
        decompressionAction.AddToCommand(decompressCommand);
        decompressCommand.Action = decompressionAction;

        rootCommand.Subcommands.Add(decompressCommand);

        return rootCommand.Parse(args).Invoke();
    }
}
