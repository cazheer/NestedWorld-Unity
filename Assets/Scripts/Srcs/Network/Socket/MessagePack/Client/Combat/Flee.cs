using System;
using System.IO;

namespace MessagePack.Client.Combat
{
    public class Flee : RequestBase
    {
        public Flee()
            : base("combat:flee")
        {

        }

        public override MemoryStream GetStream()
        {
            throw new NotImplementedException();
        }
    }
}
