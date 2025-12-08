namespace Jtar.Decompression;

public class DecompressionContextBuilder
{
    private int _threadCount = Environment.ProcessorCount - 1;
    private string _inputFile = "";
    private string _outputDir = "output";

    public DecompressionContextBuilder SetThreadCount(int threadCount)
    {
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
        return new DecompressionContext(data, _outputDir, _threadCount);
    }
}