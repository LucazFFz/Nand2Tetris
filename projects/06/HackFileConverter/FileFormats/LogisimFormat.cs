using System.ComponentModel;
using System.Text;
using HackFileConverter.Extensions;

namespace HackFileConverter.FileFormats;

public class LogisimFormat : IFileFormat
{
    public List<string> ConvertToHack(List<string> hack)
    {
        const int instructionsPerLine = 8;

        var lines = new List<string>()
        {
            "v2.0 raw"
        };

        var partitionedChunks = hack.ChunkBy(x => x).Chunk(instructionsPerLine);

        foreach (var chunks in partitionedChunks)
        {
            var line = new StringBuilder();
            
            foreach (var chunk in chunks)
                line.Append(ToLogisimFormat(chunk));
            
            lines.Add(line.ToString().Trim());
        }
        
        return lines;
    }

    private string ToLogisimFormat(IGrouping<string, string> group)
    {
        var element = new StringBuilder();
        var hex = Utils.BinaryToHexadecimal(group.Key);
        var amount = group.Count();
        
        if(amount == 1 && hex != "0") 
            hex = hex.TrimStart('0');
        
        if (amount >= 4) 
            return $"{amount}*{hex} ";

        foreach (var _ in group)
            element.Append($"{hex} ");
        
        return element.ToString();
    }
}

