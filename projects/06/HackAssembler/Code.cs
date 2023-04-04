public class Code 
{
    private static IReadOnlyDictionary<string, byte> _jumpCodes
        = new Dictionary<string, byte> 
            {
                {"", 0x000},        // no jump
                {"JGT", 0x001},     // if out > 0, jump
                {"JEQ", 0x010},     // if out == 0, jump
                {"JGE", 0x011},     // if out >= 0, jump
                {"JLT", 0x100},     // if out < 0, jump
                {"JNE", 0x101},     // if out != 0, jump
                {"JLE", 0x110},     // if out <= 0, jump
                {"JMP", 0x111}      // unconditional jump
            };

    private static IReadOnlyDictionary<string, byte> _destCodes
        = new Dictionary<string, byte> 
            {
                {"", 0x000},        // the value os not stored
                {"M", 0x001},       // RAM[A]
                {"D", 0x010},       // D register
                {"MD", 0x011},      // RAM[A] and D register
                {"A", 0x100},       // A register
                {"AM", 0x101},      // A register and RAM[A]
                {"AD", 0x110},      // A register and D register
                {"AMD", 0x111}      // A register, RAN[A] and D register
            };

    private static IReadOnlyDictionary<string, byte> _compCodes 
        = new Dictionary<string, byte> 
            {
                {"0", 0x0101010},
                {"1", 0x0111111},
                {"-1", 0x011010},
                {"D", 0x0001100},
                {"A", 0x0110000},
                {"M", 0x1110000},
                {"!D", 0x0001101},
                {"!A", 0x0110001},
                {"!M", 0x1110001},
                {"-D", 0x0001111},
                {"-A", 0x0110011},
                {"-M", 0x1110011},
                {"D+1", 0x0011111},
                {"A+1", 0x0110111},
                {"M+1", 0x1110111},
                {"D-1", 0x0001110},
                {"A-1", 0x0110010},
                {"M-1", 0x1110010},
                {"D+A", 0x0000010},
                {"D+M", 0x1000010},
                {"D-A", 0x0010011},
                {"D-M", 0x1010011},
                {"A-D", 0x0000111},
                {"M-D", 0x1000111},
                {"D&A", 0x0000000},
                {"D&M", 0x1000000},
                {"D|A", 0x0010101},
                {"D|M", 0x1010101}
            };

    public byte[][] translateAInstruction(string[] token) 
    {

    }
    public byte[][] translateCInstruction(string[] token) 
    {

    }
}