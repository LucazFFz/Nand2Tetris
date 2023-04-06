using System.Data;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using HackAssembler.Core.Instructions;

namespace HackAssembler.Core 
{
    public static class Parser
    {
        public static IInstruction ToInstruction(string str, SymbolDictionary symbols)
        {
            if (IsAInstruction(str)) return ParseAInstruction(str, symbols);
            if (IsCInstruction(str)) return ParseCInstruction(str);

            throw new FormatException("Not an instruction");
        }

        private static IInstruction ParseAInstruction(string assemblyAInstruction, SymbolDictionary symbols)
        {
            if (!IsAInstruction(assemblyAInstruction)) 
                throw new FormatException("Not an A instruction");
            
            var value = assemblyAInstruction.Remove(0, 1);
            return new AInstruction(value, symbols);
        }
        
        private static IInstruction ParseCInstruction(string assemblyCInstruction)
        {
            if (!IsCInstruction(assemblyCInstruction)) 
                throw new FormatException("Not a C instruction");
            
            //TODO: change to C# library implementation instead of custom regex
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
