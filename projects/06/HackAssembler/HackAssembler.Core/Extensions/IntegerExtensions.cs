namespace HackAssembler.Core.Extensions;

public static class IntegerExtensions
{
    public static string To16BitBinary(this int value)
    {
        var binary = Convert.ToString(value, 2);
        return binary.PadLeft(16, '0');
    }
}