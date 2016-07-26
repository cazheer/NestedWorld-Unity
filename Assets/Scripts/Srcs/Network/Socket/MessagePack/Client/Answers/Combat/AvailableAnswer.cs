using MessagePack.Serveur;
using System;
using System.Collections.Generic;
using System.IO;

namespace MessagePack.Client.Answers.Combat
{
    public class AvailableAnswer : RequestBase
    {
        public bool accept { get; set; }
        public int[] monsters = new int[4];
        public AvailableAnswer() : base("authenticate") { }

        public override MemoryStream GetStream()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["type"] = type;
            dic["id"] = id;
            dic["accept"] = monsters;

            MemoryStream stream = new MemoryStream();
            Packager.Pack(stream, dic);
            stream.Position = 0;
            return stream;
        }
    }
}
