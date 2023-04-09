using HackFileConverter.Exceptions;
using HackFileConverter.FileFormats;

namespace HackFileConverter;

public class ConverterRunner
{
    private readonly string _inputFilePath;
    private readonly string _outputFilePath;
    private readonly IFileFormat _fileFormat;
    
    public ConverterRunner(string inputFilePath, string outputFilePath, string fileExtension)
    {
        if (!Converter.SupportedFileFormats.TryGetValue(fileExtension, out var format))
            throw new InvalidExtensionException("Unsupported file extension");

        _fileFormat = format;
        _inputFilePath = inputFilePath;
        _outputFilePath = outputFilePath;
    }

    public void Run()
    {
        using var reader = File.OpenRead(_inputFilePath);
        using var writer = new StreamWriter(_outputFilePath, false);
        var converter = new Converter();
        converter.Convert(new StreamReader(reader), writer, _fileFormat);
    }
}