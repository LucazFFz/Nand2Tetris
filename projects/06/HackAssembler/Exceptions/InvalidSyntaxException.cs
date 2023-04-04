[Serializable]
public class InvalidSyntaxException : Exception
{
    public string Line { get; }

    public InvalidSyntaxException() { }

    public InvalidSyntaxException(string message)
        : base(message) { }

    public InvalidSyntaxException(string message, Exception inner)
        : base(message, inner) { }

    public InvalidSyntaxException(string message, string line)
        : this(message)
    {
        Line = line;
    }
}