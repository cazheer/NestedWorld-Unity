using System.Collections.Generic;
using System.IO;

namespace MessagePack.Client.Combat
{
    public class Capture : RequestBase
    {
        public bool capture { get; set; }
        public string name { get; set; }

        public Capture()
            : base("combat:monster-ko:capture")
        { }

        public override MemoryStream GetStream()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["type"] = type;
            dic["id"] = id;
            dic["capture"] = capture;
            dic["name"] = name;

            MemoryStream stream = new MemoryStream();
            Packager.Pack(stream, dic);
            stream.Position = 0;
            return stream;
        }
    }
}
