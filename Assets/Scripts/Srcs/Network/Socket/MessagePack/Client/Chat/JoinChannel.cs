using MessagePack.Client.Answers.Chat;
using System.Collections.Generic;
using System.IO;

namespace MessagePack.Client.Chat
{
    public class JoinChannel : RequestBase
    {
       

        public string channel;

        public JoinChannel()
            : base("chat:join-channe", typeof(JoinChannelAnswers)) { }

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

        public static JoinChannel Join(string Channel)
        {
            return new JoinChannel() { channel = Channel };
        }
    }
}
