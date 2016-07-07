namespace Network.MessagePack
{
    public abstract class MessageBase
    {
        public string type;
        public string id;

        public MessageBase(string type, string id)
        {
            this.type = type;
            this.id = id;
        }
        public MessageBase()
        {

        }
    }
}

