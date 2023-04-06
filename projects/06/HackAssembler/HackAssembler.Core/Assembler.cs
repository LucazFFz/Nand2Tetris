using HackAssembler.Core.Exceptions;
using HackAssembler.Core.Extensions;

namespace HackAssembler.Core
{
    public class Assembler
    {
        public void Assemble(StreamReader reader, StreamWriter writer)
        {
            var currentLineNumber = 1;

            var symbols = new SymbolDictionary();
            symbols.AddAllLabelSymbolsFromFile(reader);

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine()!.Trim().StripComment("//");

                if (line.IsEmpty() || line.IsLabel())
                {
                    currentLineNumber++;
                    continue;
                }

                try
                {
                    var machineCode = ConvertLineToMachineCode(line, symbols);
                    writer.WriteLine(machineCode);
                }
                catch (FormatException e)
                {
                    throw new AssemblyException($"Syntax error on line {currentLineNumber}", e);
                }
                
                currentLineNumber++;
            }
        }

        private static string? ConvertLineToMachineCode(string line, SymbolDictionary symbols)
        {
            var instruction = Parser.ToInstruction(line, symbols);
            return instruction.ToMachineCode();
        }
    }
}