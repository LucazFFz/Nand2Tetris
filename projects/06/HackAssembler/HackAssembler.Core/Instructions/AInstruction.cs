 using HackAssembler.Core.Extensions;

 namespace HackAssembler.Core.Instructions
{
    public class AInstruction : IInstruction
    {
        private readonly string _value;
        private readonly SymbolDictionary _symbols;

        public AInstruction(string value, SymbolDictionary symbols)
        {
            _symbols = symbols;
            _value = value;
        }
        public string ToMachineCode()
        {
            int address;
            
            var isNumeric = int.TryParse(_value, out address);
            if (isNumeric) return address.To16BitBinary();
           
            var symbolExists = _symbols.TryGetAddress(_value, out address);
            if (symbolExists) return address.To16BitBinary();
            
            address = _symbols.CurrentVariableAddress;
            AddVariableSymbol(address);
            return address.To16BitBinary();
        }

        private void AddVariableSymbol(int address)
        {
            _symbols.TryAdd(_value, address);
            _symbols.IncrementCurrentVariableAddress();
        }
    }
}

