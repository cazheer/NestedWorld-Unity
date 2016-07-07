using System.IO;
using System.Collections.Generic;

namespace MessagePack.Client.Combat
{
    public class Replace : RequestBase
    {

        public int UserMonsterId { get; set; }

        public Replace() 
            : base("combat:monster-ko:replace")
        {
        }

        public override MemoryStream GetStream()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["type"] = type;
            dic["id"] = id;
            dic["user_monster_id"] = UserMonsterId;

            MemoryStream stream = new MemoryStream();
            Packager.Pack(stream, dic);
            stream.Position = 0;
            return stream;
        }

        public static Replace ReplaceMonster(int MonsterID)
        {
            return new Replace() { UserMonsterId = MonsterID };
        }
    }
}
