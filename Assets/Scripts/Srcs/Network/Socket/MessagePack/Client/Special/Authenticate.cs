using System.Collections.Generic;
using System.IO;

namespace MessagePack.Client.Special
{
    public class Authenticate : RequestBase
    {
        public string token;
        public Authenticate() : base("authenticate") { }

        public override MemoryStream GetStream()
        {
            Logger.Instance.Log("token: " + token);

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["type"] = type;
            dic["id"] = id;
            dic["token"] = token;

            MemoryStream stream = new MemoryStream();
            Packager.Pack(stream, dic);
            stream.Position = 0;
            return stream;
        }

        public static Authenticate GetAuthenticate(string Token)
        {
            return new Authenticate() { token = Token };
        }
    }
}
