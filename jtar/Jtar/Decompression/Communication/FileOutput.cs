using System.Formats.Tar;
using Jtar.Logging;

namespace Jtar.Decompression.Communication;

public class FileOutput : IOutput<TarEntry>
{
    private readonly bool _overwrite;
    private readonly string _rootPath;

    public FileOutput(string rootPath = ".", bool overwrite = true)
    {
        _overwrite = overwrite;
        _rootPath = Path.GetFullPath(rootPath);
    }

    public void Put(TarEntry obj)
    {
        var finalPath = $"{_rootPath}/{obj.Name}";
        //Logger.Log(LogType.Debug, $"File outputed to {finalPath}");
        // ! Uncomment !
        Directory.CreateDirectory(Path.GetDirectoryName(finalPath));
        obj.ExtractToFile(finalPath, _overwrite);
    }
}