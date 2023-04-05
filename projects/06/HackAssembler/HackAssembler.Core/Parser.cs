using System.Text.RegularExpressions;
using HackAssembler.Core.Instructions;

namespace HackAssembler.Core 
{
    public static class Parser
    {
        private static readonly Dictionary<Type, Tuple<Func<string, IInstruction>, Func<string, bool>>>
            InstructionTypes = new()
            {
                {
                    typeof(AInstruction),
                    new Tuple<Func<string, IInstruction>, Func<string, bool>>(ParseAInstruction, IsAInstruction)
                },
                {
                    typeof(CInstruction),
                    new Tuple<Func<string, IInstruction>, Func<string, bool>>(ParseCInstruction, IsCInstruction)
                }
            };

        public static Type? GetInstructionType(string line) 
            => InstructionTypes.FirstOrDefault(x 
                => x.Value.Item2.Invoke(line)).Key;
        
        public static IInstruction? ParseFromLine(string line)
        {
            var type = GetInstructionType(line);
            if (type is null) return null;
            
            InstructionTypes.TryGetValue(type, out var value);
            return value?.Item1.Invoke(line);
        }
        
        public static string RemoveWhiteSpaceAndComments(string line)
        {
            const string pattern = @"\s+|\/{2}.*";
            return Regex.Replace(line, pattern, "");
        }

        private static IInstruction ParseAInstruction(string line)
        {
            const string pattern = @"\w+";
            string value = Regex.Match(line, pattern).Value;

            return new AInstruction(value);
        }
        
        private static CInstruction ParseCInstruction(string line)
        {
            const string jumpPattern = @"(?<=\;).*$",
                compPattern = @"((?<=\=)|^)[^;=]+((?=\;)|$)",
                destPattern = @"^.*(?=\=)";
            
            var destInstruction = Regex.Match(line, destPattern).Value;
            var compInstruction = Regex.Match(line, compPattern).Value;
            var jumpInstruction = Regex.Match(line, jumpPattern).Value;

            return new CInstruction(destInstruction, compInstruction, jumpInstruction);
        }

        private static bool IsCInstruction(string line)
        {
            const string pattern = @"^\w?=?.+;?\w*";
            return Regex.IsMatch(line, pattern);
        }
        
        private static bool IsAInstruction(string line)
        {
            const string pattern = @"^@[\w\d]*";
            return Regex.IsMatch(line, pattern);
        }
    }
}
