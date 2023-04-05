namespace HackAssembler.Core.Exceptions
{
    public class AssemblyException : Exception
    {
        public AssemblyException()
        {
        }

        public AssemblyException(string message)
            : base(message)
        {
        }

        public AssemblyException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

