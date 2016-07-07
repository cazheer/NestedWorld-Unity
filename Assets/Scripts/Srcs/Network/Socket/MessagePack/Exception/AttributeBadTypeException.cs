using System;

namespace MessagePack.Exception
{
    public class AttributeBadTypeException : System.Exception
    {
        private const string messageBase = "the attribute searching has found, but is not the good type. the correct type of {0} is {1}";
        
        public AttributeBadTypeException(string attributeName, Type type)
         :base (string.Format(messageBase, attributeName, type.ToString()))
        { }
    }
}
