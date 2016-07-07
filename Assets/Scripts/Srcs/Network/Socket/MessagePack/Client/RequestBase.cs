using Network.MessagePack;
using System;
using System.Diagnostics;
using System.IO;

namespace MessagePack.Client
{
    public abstract class RequestBase : MessageBase
    {
        public Type AnswerType { get; private set; }

        private static MsgPack.BoxingPacker packager = null;
        protected static MsgPack.BoxingPacker Packager
        {
            get
            {
                if (packager == null)
                    packager = new MsgPack.BoxingPacker();
                return packager;
            }
        }

        public RequestBase(string type, Type AnswerType)
            : base(type, NewGuid()) {
            this.AnswerType = AnswerType;
        }


        public RequestBase(string type)
            : base(type, NewGuid())
        {
            this.AnswerType = null;
        }
        internal static string NewGuid()
        {
            Guid g = Guid.NewGuid();
            try
            {
                return g.ToString("B");
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return "null";
        }

         public abstract MemoryStream GetStream();
    }
}
