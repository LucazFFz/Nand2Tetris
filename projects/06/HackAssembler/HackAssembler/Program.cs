using System.Data;
using HackAssembler.Core;

string[] arguments = Environment.GetCommandLineArgs();

if (arguments.Length < 3)
{
    Console.WriteLine("Missing arguments...");
    Console.WriteLine("Example Usage:");
    Console.WriteLine("HackAssembler \"path/to/asm/file.asm\"  \"output/directory/path/\"");
    return;
}

var inputFilePath = arguments[1];
var outputDirectory = Path.GetDirectoryName(arguments[1]);
if (outputDirectory is null)
{
    Console.WriteLine("The input path must contain a directory");
}

var outputFileName = $"{Path.GetFileNameWithoutExtension(inputFilePath)}.hack";
var outputFilePath = Path.Combine(outputDirectory!, outputFileName);

try
{
    var runner = new AssemblerRunner(inputFilePath, outputFilePath);
    runner.Run();
    
    Console.WriteLine($"Assembly successful, output file path: {outputFilePath}");
}
catch (SyntaxErrorException e)
{
    Console.WriteLine($"An error occurred during assembly: {e} ");
}
    
   




