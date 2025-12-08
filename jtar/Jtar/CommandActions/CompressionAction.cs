using System.CommandLine;
using System.CommandLine.Invocation;
using Jtar.Compression;
using Jtar.Compression.FileLoader;
using Jtar.Compressor;
using Jtar.Exceptions;
using Jtar.Logging;

namespace Jtar.CommandActions;

public class CompressionAction : SynchronousCommandLineAction
{
    private readonly Option<int> threadCountOption;
    private readonly Option<string> outputFileOption;
    private readonly Argument<IEnumerable<string>> fileArgument;
    private readonly Option<bool> debugOption;
    private readonly Option<bool> noCompressionOption;
    private readonly Option<bool> ustarOption;

    public CompressionAction()
    {
        threadCountOption = new Option<int>("--thread-count", "-t")
        {
            Description = "The amount of threads used for compression"
        };

        outputFileOption = new Option<string>("--output", "-o")
        {
            Description = "The filepath where the resulting file will be saved"
        };

        fileArgument = new Argument<IEnumerable<string>>("file")
        {
            Description = "The file to package"
        };

        debugOption = new Option<bool>("--debug", "-d")
        {
            Description = "Print out debug information"
        };

        noCompressionOption = new Option<bool>("--no-compress", "-n")
        {
            Description = "Disables compression"
        };

        ustarOption = new Option<bool>("--ustar", "-u")
        {
            Description = "Uses USTAR TAR format"
        };
    }

    public void AddToCommand(Command command)
    {
        command.Options.Add(threadCountOption);
        command.Options.Add(outputFileOption);
        command.Arguments.Add(fileArgument);
        command.Options.Add(debugOption);
        command.Options.Add(noCompressionOption);
        command.Options.Add(ustarOption);
    }

    public override int Invoke(ParseResult parseResult)
    {
        if (parseResult.Errors.Count == 0 && parseResult.GetValue(fileArgument) is IEnumerable<string> files)
        {
            if (files.Count() < 1)
            {
                Logger.Log(LogType.Error, "No files specified!");
                return 1;
            }
            Logger.ShowDebugMessages = parseResult.GetValue(debugOption);
            CompressionContextBuilder builder = new CompressionContextBuilder();
            builder.SetInputFiles(files);

            string outputName = "";
            foreach (var file in files)
            {
                if (!File.Exists(file) && !Directory.Exists(file))
                    continue;
                if (Path.IsPathRooted(file))
                    continue;
                outputName = file + ".tar";
            }
            if (string.IsNullOrWhiteSpace(outputName))
            {
                Logger.Log(LogType.Error, "All input files are invalid!");
                return 1;
            }
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

            var isUstar = parseResult.GetValue(ustarOption);
            ITarFormatter tarFormatter;
            if (isUstar)
            {
                tarFormatter = new UstarTarFormatter();
            }
            else
            {
                tarFormatter = new PaxTarFormatter();
            }
            builder.SetTarFormatter(tarFormatter);

            var argOutputName = parseResult.GetValue(outputFileOption);
            if (argOutputName != null && argOutputName != string.Empty) outputName = argOutputName;

            var threadCount = parseResult.GetValue(threadCountOption);
            if (threadCount > 0) builder.SetThreadCount(threadCount);

            builder.SetOutputFile(outputName);

            try
            {
                Logger.Log(LogType.Info, $"Creating archive: {outputName}");
                var context = builder.Build();
                Task.Run(context.Compress).Wait();
                Logger.Log(LogType.Info, $"Archive created: {outputName}");

            }
            catch (InvalidOutputFileException e)
            {
                Logger.Log(LogType.Error, e.Message);
                return 1;
            }
            catch (Exception e)
            {
                Logger.Log(LogType.Error, "An unknown error occured!");
                Logger.Log(LogType.Debug, e.ToString());
                return 1;
            }
            return 0;
        }
        return 1;
    }
}