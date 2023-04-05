using System.Data;
using System.Runtime.CompilerServices;
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

        public static Type? TryGetInstructionType(this string assemblyInstruction) 
            => InstructionTypes.FirstOrDefault(x 
                => x.Value.Item2.Invoke(assemblyInstruction)).Key;
        
        public static IInstruction ToInstruction(this string assemblyInstruction)
        {
            var type = assemblyInstruction.TryGetInstructionType();
            
            if (type is null) throw new FormatException("Not an instruction");
            
            InstructionTypes.TryGetValue(type, out var value);
            return value!.Item1.Invoke(assemblyInstruction);
        }
        
        public static string TrimWhiteSpaceAndComments(this string line) 
            => Regex.Replace(line, @"\s+|\/{2}.*", string.Empty);

        private static IInstruction ParseAInstruction(string assemblyAInstruction)
        {
            if (assemblyAInstruction.TryGetInstructionType() != typeof(AInstruction)) 
                throw new FormatException("Not an A instruction");
            
            var value = assemblyAInstruction.Remove(0, 1);
            return new AInstruction(value);
        }
        
        private static IInstruction ParseCInstruction(string assemblyCInstruction)
        {
            if (assemblyCInstruction.TryGetInstructionType() != typeof(CInstruction)) 
                throw new FormatException("Not a C instruction");
            
            /*
            string dest, comp, jump;

            char[] delimiterChars = {'=', ';'};
            var splitInstruction = assemblyCInstruction.Split(delimiterChars);
            
            if(splitInstruction.Length > 3)
                throw new FormatException("Not an C instruction");
            
            

            return new CInstruction(dest, comp, jump);
            */
            
            const string jumpPattern = @"(?<=\;).*$",
                compPattern = @"((?<=\=)|^)[^;=]+((?=\;)|$)",
                destPattern = @"^.*(?=\=)";
            
            var destInstruction = Regex.Match(assemblyCInstruction, destPattern).Value;
            var compInstruction = Regex.Match(assemblyCInstruction, compPattern).Value;
            var jumpInstruction = Regex.Match(assemblyCInstruction, jumpPattern).Value;

            return new CInstruction(destInstruction, compInstruction, jumpInstruction);
        }

        private static bool IsCInstruction(string assemblyInstruction) => !assemblyInstruction.StartsWith("@");

        private static bool IsAInstruction(string assemblyInstruction) => assemblyInstruction.StartsWith("@");
    }
}
