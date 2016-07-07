namespace MessagePack.Exception
{
    public class NoTypeFoundException : System.Exception
    {
        private const string messagebase = "No type found for ";
        public NoTypeFoundException(string content) : base(messagebase + content) { }
    }
}
