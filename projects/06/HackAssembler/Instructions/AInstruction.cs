namespace HackAssembler.Instructions
{
    public class AInstruction : IInstruction
    {
        private readonly string _value;

        public AInstruction(string value)
        {
            _value = value;
        }

        public string ConvertToMachineCode()
        {
            var value = Int32.Parse(_value);
            var binary = Convert.ToString(value, 2);
            return binary.PadLeft(16, '0');
        }
    }
}

