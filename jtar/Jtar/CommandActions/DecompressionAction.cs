using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics;
using Jtar.Decompression;
using Jtar.Exceptions;
using Jtar.Logging;

namespace Jtar.CommandActions;

public class DecompressionAction : SynchronousCommandLineAction
{
    private readonly Option<int> threadCountOption;
    private readonly Option<string> outputDirOption;
    private readonly Argument<string> fileArgument;
    private readonly Option<bool> debugOption;

    public DecompressionAction()
    {
        threadCountOption = new Option<int>("--thread-count", "-t")
        {
            Description = "The amount of threads used for compression"
        };

        outputDirOption = new Option<string>("--output", "-o")
        {
            Description = "The directory where the unpacked archive will be saved"
        };

        fileArgument = new Argument<string>("file")
        {
            Description = "The file to decompress"
        };

        debugOption = new Option<bool>("--debug", "-d")
        {
            Description = "Print out debug information"
        };
    }

    public void AddToCommand(Command command)
    {
        command.Options.Add(threadCountOption);
        command.Options.Add(outputDirOption);
        command.Arguments.Add(fileArgument);
        command.Options.Add(debugOption);
    }

    public override int Invoke(ParseResult parseResult)
    {
        if (parseResult.Errors.Count == 0 && parseResult.GetValue(fileArgument) is string file)
        {
            if (!File.Exists(file))
            {
                Logger.Log(LogType.Error, "Archive file does not exist!");
                return 1;
            }
            Logger.ShowDebugMessages = parseResult.GetValue(debugOption);
            DecompressionContextBuilder builder = new DecompressionContextBuilder();
            builder.SetInputFile(file);

            string filename = Path.GetFileName(file);
            string outputDir = Path.GetDirectoryName(file) + filename.Substring(0, filename.IndexOf('.'));

            var argOutputName = parseResult.GetValue(outputDirOption);
            if (argOutputName != null && argOutputName != string.Empty) outputDir = argOutputName;

            var threadCount = parseResult.GetValue(threadCountOption);
            if (threadCount > 0) builder.SetThreadCount(Math.Max(threadCount, 1));

            builder.SetOutputDir(outputDir);

            try
            {
                Logger.Log(LogType.Info, $"Unpacking archive into directory: {outputDir}");
                var context = builder.Build();
                Task.Run(context.Decompress).Wait();
                Logger.Log(LogType.Info, $"Archive unpacked!");

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