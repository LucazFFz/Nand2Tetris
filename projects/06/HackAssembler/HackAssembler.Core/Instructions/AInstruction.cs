using System.Text.RegularExpressions;

namespace HackAssembler.Core.Instructions
{
    public class AInstruction : IInstruction
    {
        private readonly string _value;

        public AInstruction(string value)
        {
            _value = value;
        }
        
        public string ToMachineCode()
        {
            var value = int.Parse(_value);
            var binary = Convert.ToString(value, 2);
            return binary.PadLeft(16, '0');
        }
    }
}

