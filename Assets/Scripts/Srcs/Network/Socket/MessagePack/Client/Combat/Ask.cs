using System.Collections.Generic;
using System.IO;

namespace MessagePack.Client.Combat
{
    public class Ask : RequestBase
    {
        public string opponent { get; set; }

        public Ask()
            : base("combat:monster-ko:capture")
        { }

        public override MemoryStream GetStream()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["type"] = type;
            dic["id"] = id;
            dic["opponent"] = opponent;

            MemoryStream stream = new MemoryStream();
            Packager.Pack(stream, dic);
            stream.Position = 0;
            return stream;
        }
    }
}