using HackFileConverter.Extensions;

namespace HackFileConverter.FileFormats;

public class LogisimFormat : IFileFormat
{
    public List<string> ConvertToHack(List<string> hack)
    {
        var result = ToLogisimFormat(MergeContinuousElements(hack));

        var list = new List<string>()
        {
            "v2.0 raw"
        };
        result.Partition(8).ToList().ForEach(x => list.Add(string.Join(" ", x)));

        return list;
    }

    private List<string> ToLogisimFormat(List<Tuple<string, int>> list)
    {
        var formatted = new List<string>();
        foreach (var element in list)
        {
            var hex = Utils.BinaryToHexadecimal(element.Item1);
            var amount = element.Item2;

            if (amount == 1 && hex != "0")
                hex = hex.TrimStart('0');

            if (amount >= 4)
            {
                formatted.Add($"{amount}*{hex}");
                continue;
            }
            
            formatted.Add(hex);
        }

        return formatted;
    }

    private List<Tuple<string, int>> MergeContinuousElements(List<string> list)
    {
        var elements = new List<Tuple<string, int>>();
        List<string> accumulate = new List<string>();
        
        for (int i = 0; i < list.Count; i++)
        {
            var element = list[i];
            
            if (accumulate.Count == 0 || accumulate[0] == element)
                accumulate.Add(element);

            if (i != list.Count - 1 && list[i + 1] == element)
                continue;
            
            if (accumulate.Count >= 4)
            {
                elements.Add(new Tuple<string, int>(element, accumulate.Count));
                accumulate = new List<string>();
                continue;
            }
            
            accumulate.ForEach(x => elements.Add(new Tuple<string, int>(x, 1)));
            accumulate = new List<string>();
        }

        return elements;
    }
}

