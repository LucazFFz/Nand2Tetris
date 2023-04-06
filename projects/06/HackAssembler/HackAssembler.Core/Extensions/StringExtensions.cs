using System.Text.RegularExpressions;

namespace HackAssembler.Core.Extensions
{
    public static class StringExtensions
    {
        public static string StripComment(this string str, string commentIndicator) 
            => Regex.Replace(str, @$"\{commentIndicator}.*", string.Empty).TrimEnd();
        
        public static bool IsLabel(this string str) =>  str.StartsWith("(") && str.EndsWith(")");

        public static bool IsEmpty(this string str) => str == string.Empty;

        public static string BetweenCharacters(this string str, char startChar, char endChar) 
            => Regex.Match(str, $@"(?<=\{startChar}).*(?=\{endChar})").Value;
    }
}

