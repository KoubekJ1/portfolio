using System.CommandLine;
using System.Threading.Tasks;
using Jtar.Compression;
using Jtar.Compression.Compressor;
using Jtar.Logging;

namespace Jtar;

class Program
{
    static async Task<int> Main(string[] args)
    {
        RootCommand rootCommand = new("jtar");

        Option<int> threadCountOption = new("--threadCount", "-t")
        {
            Description = "The amount of threads used for compression"
        };
        rootCommand.Options.Add(threadCountOption);

        Option<string> outputFileOption = new("--output", "-o")
        {
            Description = "The filepath where the resulting file will be saved"
        };
        rootCommand.Options.Add(outputFileOption);

        var fileArgument = new Argument<IEnumerable<string>>("file")
        {
            Description = "The file to package"
        };
        rootCommand.Arguments.Add(fileArgument);

        var debugOption = new Option<bool>("--debug", "-d")
        {
            Description = "Print out debug information"
        };
        rootCommand.Options.Add(debugOption);

        var noCompressionOption = new Option<bool>("--noCompression", "-n")
        {
            Description = "Disables compression"
        };
        rootCommand.Options.Add(noCompressionOption);

        ParseResult parseResult = rootCommand.Parse(args);
        if (parseResult.Errors.Count == 0 && parseResult.GetValue(fileArgument) is IEnumerable<string> files)
        {
            if (files.Count() < 1)
            {
                Logger.Log(LogType.Error, "No files specified!");
                return 1;
            }
            Logger.ShowDebugMessages = parseResult.GetValue(debugOption);
            Compression.CompressionContextBuilder builder = new CompressionContextBuilder();
            builder.SetInputFiles(files);

            var outputName = files.First() + ".tar";
            var isCompression = !parseResult.GetValue(noCompressionOption);
            ICompressor compressor;
            if (isCompression)
            {
                outputName += ".zstd";
                compressor = new ZstdCompressor();
            }
            else
            {
                compressor = new NoCompressor();
            }
            builder.SetCompressor(compressor);

            var argOutputName = parseResult.GetValue(outputFileOption);
            if (argOutputName != null && argOutputName != string.Empty) outputName = argOutputName;

            var threadCount = parseResult.GetValue(threadCountOption);
            if (threadCount > 0) builder.SetThreadCount(threadCount);

            builder.SetOutputFile(outputName);

            var context = builder.Build();
            await context.Compress();
            return 0;
        }
        return 1;
    }
}
