using System.Data;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic.CompilerServices;

namespace HackAssembler
{
    public class Assembler
    { 
        public void Assemble(StreamReader reader, StreamWriter writer)
        {
            var currentLineNumber = 1;

            while (!reader.EndOfStream)
            {
                var line = Parser.RemoveWhiteSpaceAndComments(reader.ReadLine()!);
                
                if (string.IsNullOrEmpty(line))
                {
                    currentLineNumber++;
                    continue;
                }

                try
                {
                    var machineCode = ConvertLineToMachineCode(line);
                    writer.WriteLine(machineCode);
                }
                catch (SyntaxErrorException e)
                {
                    throw new SyntaxErrorException($"Syntax error on line {currentLineNumber}", e);
                }
                
                currentLineNumber++;
            }
        }

        private static string? ConvertLineToMachineCode(string line)
        {
            var instruction = Parser.ParseFromLine(line);

            if (instruction is null) 
                throw new SyntaxErrorException();
            
            return instruction.ConvertToMachineCode();
        }
    }
}