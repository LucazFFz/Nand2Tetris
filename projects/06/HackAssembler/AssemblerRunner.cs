namespace HackAssembler
{
    public class AssemblerRunner
    {
        private readonly string _inputFilePath;
        private readonly string _outputFilePath;

        public AssemblerRunner(string inputFilePath, string outputFilePath)
        {
            _inputFilePath = inputFilePath;
            _outputFilePath = outputFilePath;
        }

        public void Run()
        {
            using var reader = File.OpenRead(_inputFilePath);
            using var writer = new StreamWriter(_outputFilePath, false);
            var assembler = new Assembler();
            assembler.Assemble(new StreamReader(reader), writer);
        }
    }
}

