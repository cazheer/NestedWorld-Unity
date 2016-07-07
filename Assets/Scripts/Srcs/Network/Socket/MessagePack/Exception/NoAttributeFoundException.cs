namespace MessagePack.Exception
{
    public class NoAttributeFoundException : System.Exception
    {
        private const string messagebase = "the Attribute researching is not found ";

        public NoAttributeFoundException(string attributeName) :
            base (messagebase + attributeName)
        { }
    }
}
