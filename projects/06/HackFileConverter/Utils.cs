namespace HackFileConverter;
using HackFileConverter.Extensions;

public static class Utils
{
    public static string BinaryToHexadecimal(string str)
    {
        if (!str.IsBinary())
            throw new FormatException("string is not in binary");
        
        var value = Convert.ToInt32(str, 2);
        var hex = Convert.ToString(value, 16);
        return hex;
    }
}