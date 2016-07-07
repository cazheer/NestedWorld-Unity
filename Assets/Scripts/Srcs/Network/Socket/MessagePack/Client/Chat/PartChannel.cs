using System.Collections.Generic;
using System.IO;

namespace MessagePack.Client.Chat
{
    public class PartChannel : RequestBase
    {
     

        public string channel;

        public PartChannel() 
            : base("chat:part-channel") { }

        public override MemoryStream GetStream()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["type"] = type;
            dic["id"] = id;
            dic["channel"] = channel;

            MemoryStream stream = new MemoryStream();
            Packager.Pack(stream, dic);
            stream.Position = 0;
            return stream;
        }

        public static PartChannel Part(string Channel)
        {
            return new PartChannel() { channel = Channel };
        }
    }
}
