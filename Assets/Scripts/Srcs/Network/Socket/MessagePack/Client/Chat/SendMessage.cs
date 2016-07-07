using System.Collections.Generic;
using System.IO;

namespace MessagePack.Client.Chat
{
    public class SendMessage : RequestBase
    {
      

        public string channel;

        public string message;

        public SendMessage()
            : base("chat:send-message") { }

        public override MemoryStream GetStream()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["type"] = type;
            dic["id"] = id;
            dic["channel"] = channel;
            dic["message"] = message;

            MemoryStream stream = new MemoryStream();
            Packager.Pack(stream, dic);
            stream.Position = 0;
            return stream;
        }

        public static SendMessage Send(string Channel, string Message)
        {
            return new SendMessage() { channel = Channel, message = Message };
        }
    }
}
