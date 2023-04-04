public class Assembler 
{   
    string FilePath { get; set; }

    private string[] _lines => File.ReadAllLines(FilePath);

    private readonly Parser _parser = new Parser();
    private readonly Code _code = new Code();

    public void Assemble(string filePath) 
    {
        FilePath = filePath;

       
    }

    private bool HasMoreCommands() 
    {
       
    }

}