namespace MessagePack.Exception
{
    public class NoDictionaryFoundException  : System.Exception
    {
        public NoDictionaryFoundException() :
            base ("The message recive is not a Dictionary")
        { }
    }
}
