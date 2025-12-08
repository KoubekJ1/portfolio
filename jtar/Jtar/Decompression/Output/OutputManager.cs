using Jtar.Decompression.Communication;

namespace Jtar.Decompression.Output;

public class OutputManager
{
    private readonly IInput<DecompressionChunk> _input;
    private readonly string _rootPath;

    public OutputManager(IInput<DecompressionChunk> input, string rootPath)
    {
        _input = input;
        _rootPath = rootPath;
    }

    public async Task Run()
    {
        var output = new FileOutput(_rootPath);
        var worker = new OutputWorker(_input, output);

        await Task.Run(worker.Run);
    }
}