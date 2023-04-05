using System.Data;
using System.Security.Cryptography.X509Certificates;
using HackAssembler.Core.Exceptions;
using HackAssembler.Core.Instructions;
using Microsoft.VisualBasic.CompilerServices;

namespace HackAssembler.Core
{
    public class Assembler
    { 
        public void Assemble(StreamReader reader, StreamWriter writer)
        {
            var currentLineNumber = 1;

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine()!.TrimWhiteSpaceAndComments();

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
                catch (FormatException e)
                {
                    throw new AssemblyException($"Syntax error on line {currentLineNumber}", e);
                }
                
                currentLineNumber++;
            }
        }

        private static string? ConvertLineToMachineCode(string line) 
            => line.ToInstruction().ToMachineCode();
    }
}