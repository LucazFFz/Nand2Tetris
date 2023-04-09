namespace HackFileConverter.Extensions;

public static class StringExtensions
{
    public static bool IsBinary(this string str) 
        => str.All(x => x is '1' or '0');
}