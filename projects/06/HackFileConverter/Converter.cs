using HackFileConverter.FileFormats;
using HackFileConverter.Extensions;

namespace HackFileConverter;

public class Converter
{
    public static readonly Dictionary<string, IFileFormat> SupportedFileFormats = new()
    {
        {".logisim", new LogisimFormat()}
    };
    
    public void Convert(StreamReader reader, StreamWriter writer, IFileFormat format)
    {
        var hackMachineCode = PopulateList(reader);
        var converted = format.ConvertToHack(hackMachineCode);

        foreach (var line in converted)
        {
            writer.WriteLine(line);
        }
    }

    private List<string> PopulateList(StreamReader reader)
    {
        var lines = new List<string>();
        
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine()!;

            if (!line.IsBinary() && line.Length == 16)
                throw new FormatException("Line is not in 16 bit binary format");
            
            lines.Add(line);
        }

        return lines;
    }
}