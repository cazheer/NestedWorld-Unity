using System.Collections.Generic;
using System.IO;

namespace MessagePack.Client.Combat
{
    public class SendAttack : RequestBase
    {
        public int target { get; set; }
        public int attack { get; set; }

        public SendAttack() : base("combat:send-attack") { }
        public override MemoryStream GetStream()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["type"] = type;
            dic["id"] = id;
            dic["target"] = target;
            dic["attack"] = attack;

            MemoryStream stream = new MemoryStream();
            Packager.Pack(stream, dic);
            stream.Position = 0;
            return stream;
        }

        public static SendAttack Attack(int attack, int target)
        {
            return new SendAttack() { attack = attack, target = target };
        }
    }
}
