namespace HackFileConverter.FileFormats;

public interface IFileFormat
{
    public List<string> ConvertToHack(List<string> hack)
    {
        throw new NotImplementedException();
    } 
}