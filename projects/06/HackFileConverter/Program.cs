// See https://aka.ms/new-console-template for more information

using HackFileConverter;
using HackFileConverter.Exceptions;

string[] arguments = Environment.GetCommandLineArgs();

if (arguments.Length < 4)
{
    Console.WriteLine("Missing arguments...");
    Console.WriteLine("Example Usage:");
    Console.WriteLine("HackAssembler \"path/to/asm/file.asm\"  \"output/directory/path/\" txt");
    return;
}

var inputFilePath = arguments[1];
var outputDirectory = Path.GetDirectoryName(arguments[1]);

if (Path.GetExtension(inputFilePath) != ".hack")
    throw new InvalidExtensionException("The input file must have the .hack extension");

if (outputDirectory is null)
    Console.WriteLine("The input path must contain a directory");

var fileExtension = arguments[3];
var outputFileName = $"{Path.GetFileNameWithoutExtension(inputFilePath)}{fileExtension}";
var outputFilePath = Path.Combine(outputDirectory!, outputFileName);

try
{
    var runner = new ConverterRunner(inputFilePath, outputFilePath, fileExtension);
    runner.Run();

    Console.WriteLine($"Conversion successful, output file path: {outputFilePath}");
}
catch (Exception e)
{
    Console.WriteLine($"An error occurred during conversion: {e} ");
}


