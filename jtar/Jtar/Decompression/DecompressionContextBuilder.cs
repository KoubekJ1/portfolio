using Jtar.Logging;

namespace Jtar.Decompression;

public class DecompressionContextBuilder
{
    private int _threadCount = Environment.ProcessorCount - 1;
    private string _inputFile = "";
    private string _outputDir = "output";

    public DecompressionContextBuilder SetThreadCount(int threadCount)
    {
        if (threadCount < 1) throw new ArgumentException("Thread count must be at least 1!");
        _threadCount = threadCount;
        return this;
    }

    public DecompressionContextBuilder SetInputFile(string inputFile)
    {
        _inputFile = inputFile;
        return this;
    }

    public DecompressionContextBuilder SetOutputDir(string outputDir)
    {
        _outputDir = outputDir;
        return this;
    }

    public DecompressionContext Build()
    {
        var data = File.ReadAllBytes(_inputFile);
        Logger.Log(LogType.Debug, $"Building decompression context builder with {_threadCount} threads");
        Logger.Log(LogType.Debug, $"File byte length: {data.Length}");
        return new DecompressionContext(data, _outputDir, _threadCount);
    }
}