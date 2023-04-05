using System.Text.RegularExpressions;
using HackAssembler.Instructions;

namespace HackAssembler 
{
    public static class Parser
    {
        private static readonly Dictionary<Type, Tuple<Func<string, IInstruction>, string>>  InstructionTypes = new ()
        {
            {typeof(AInstruction), new Tuple<Func<string, IInstruction>, string>(ParseAInstruction, @"^@[\w\d]*")},
            {typeof(CInstruction), new Tuple<Func<string, IInstruction>, string>(ParseCInstruction, @"^\w?=?.+;?\w*")}
        };

        public static Type? GetInstructionType(string line) 
            => InstructionTypes.FirstOrDefault(x 
                => Regex.IsMatch(line, x.Value.Item2)).Key;
        
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
            string value = System.Text.RegularExpressions.Regex.Match(line, pattern).Value;

            return new AInstruction(value);
        }
        
        private static CInstruction ParseCInstruction(string line)
        {
            return new CInstruction(
                GetDest(line),
                GetComp(line),
                GetJump(line));
        }
        
        private static string GetJump(string line)
        {
            const string pattern = @"(?<=\;)[A-Z]{3}$";
            var value = System.Text.RegularExpressions.Regex.Match(line, pattern).Value;

            return value;
        }
        
        private static string GetDest(string line)
        {
            const string pattern = @"^[AMD]{1,3}(?=\=)";
            var value = System.Text.RegularExpressions.Regex.Match(line, pattern).Value;

            return value;
        }
        
        private static string GetComp(string line)
        {
            const string pattern = @"((?<=\=)|^)[AMD01]?[\-\+\&\|\!]?[AMD01]?((?=\;)|$)";
            var value = System.Text.RegularExpressions.Regex.Match(line, pattern).Value;

            return value;
        }
        
    }
}
