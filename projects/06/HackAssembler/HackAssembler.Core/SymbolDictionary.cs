using System.Collections;
using System.Transactions;
using HackAssembler.Core.Extensions;

namespace HackAssembler.Core
{
    public class SymbolDictionary : DictionaryBase
    {
        public int CurrentLabelAddress { get; private set; } = 0;
        public int CurrentVariableAddress { get; private set; } = 16;

        private readonly Dictionary<string, int> _symbols = new()
        {
            {"R0", 0},
            {"R1", 1},
            {"R2", 2},
            {"R3", 3},
            {"R4", 4},
            {"R5", 5},
            {"R6", 6},
            {"R7", 7},
            {"R8", 8},
            {"R9", 9},
            {"R10", 10},
            {"R11", 11},
            {"R12", 12},
            {"R13", 13},
            {"R14", 14},
            {"R15", 15},
            {"SCREEN", 16384},
            {"KBD", 24576},
            {"SP", 0},
            {"LCL", 1},
            {"ARG", 2},
            {"THIS", 3},
            {"THAT", 4}
        };

        public bool TryAdd(string symbol, int address)
        {
            return _symbols.TryAdd(symbol, address);
        }
        
        public bool TryGetAddress(string symbol, out int address)
        {
            return _symbols.TryGetValue(symbol, out address);
        }

        public void IncrementCurrentVariableAddress() => CurrentVariableAddress++;

        public void IncrementCurrentLabelAddress() => CurrentLabelAddress++;
        
        public void AddAllLabelSymbolsFromFile(StreamReader reader)
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine()!.Trim().StripComment("//");
                
                if(line.IsEmpty()) continue;

                if (line.IsLabel())
                {
                    var label = line.BetweenCharacters('(', ')');
                    TryAdd(label, CurrentLabelAddress);
                    continue;
                }

                IncrementCurrentLabelAddress();
            }

            reader.BaseStream.Seek(0, SeekOrigin.Begin);
        }
    } 
}

