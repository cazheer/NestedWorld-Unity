namespace MessagePack.Exception
{
    public class AttributeEmptyException : System.Exception
    {
        private const string messageBaseOne = "The field ";
        private const string messageBaseTwo = " exist, but is empty";

        public AttributeEmptyException(string key)
            : base(messageBaseOne + key + messageBaseTwo) { }
    }
}
