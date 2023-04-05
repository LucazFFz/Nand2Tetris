using System.Data;
using System.Text.RegularExpressions;

namespace HackAssembler.Core.Instructions
{
    public class CInstruction : BaseInstruction
    {
        private readonly string _destMnemonic;
        private readonly string _compMnemonic;
        private readonly string _jumpMnemonic;
        
        private static readonly Dictionary<string, string> JumpTable = new ()
            {
                {"", "000"},        // no jump
                {"JGT", "001"},     // if out > 0, jump
                {"JEQ", "010"},     // if out == 0, jump
                {"JGE", "011"},     // if out >= 0, jump
                {"JLT", "100"},     // if out < 0, jump
                {"JNE", "101"},     // if out != 0, jump
                {"JLE", "110"},     // if out <= 0, jump
                {"JMP", "111"}      // unconditional jump
            };
        
            private static readonly Dictionary<string, string> DestTable = new ()
            {
                {"", "000"},        // the value os not stored
                {"M", "001"},       // RAM[A]
                {"D", "010"},       // D register
                {"MD", "011"},      // RAM[A] and D register
                {"A", "100"},       // A register
                {"AM", "101"},      // A register and RAM[A]
                {"AD", "110"},      // A register and D register
                {"AMD", "111"}      // A register, RAN[A] and D register
            };
        
            private static readonly Dictionary<string, string> CompTable = new ()
            {
                {"0", "0101010"},
                {"1", "0111111"},
                {"-1", "0111010"},
                {"D", "0001100"},
                {"A", "0110000"},
                {"M", "1110000"},
                {"!D", "0001101"},
                {"!A", "0110001"},
                {"!M", "1110001"},
                {"-D", "0001111"},
                {"-A", "0110011"},
                {"-M", "1110011"},
                {"D+1", "0011111"},
                {"A+1", "0110111"},
                {"M+1", "1110111"},
                {"D-1", "0001110"},
                {"A-1", "0110010"},
                {"M-1", "1110010"},
                {"D+A", "0000010"},
                {"D+M", "1000010"},
                {"D-A", "0010011"},
                {"D-M", "1010011"},
                {"A-D", "0000111"},
                {"M-D", "1000111"},
                {"D&A", "0000000"},
                {"D&M", "1000000"},
                {"D|A", "0010101"},
                {"D|M", "1010101"}
            };
    
        public CInstruction(string destMnemonic, string compMnemonic, string jumpMnemonic)
        {
            _destMnemonic = destMnemonic;
            _compMnemonic = compMnemonic;
            _jumpMnemonic = jumpMnemonic;
        }
    
        public override string ToMachineCode()
        {
            if (DestTable.TryGetValue(_destMnemonic, out var destMachineCode) &&
                CompTable.TryGetValue(_compMnemonic, out var compMachineCode) &&
                JumpTable.TryGetValue(_jumpMnemonic, out var jumpMachineCode))
            {
                return $"111{compMachineCode}{destMachineCode}{jumpMachineCode}";
            }

            throw new FormatException("Unrecognized instruction mnemonic");
        }
    }
}

