using MessagePack.Exception;
using MsgPack;
using System.Collections.Generic;
using System.IO;

namespace Network.MessagePack
{
    public class ReceiveMessage
    {
        public string Type;
        private MemoryStream _Stream;
        public Dictionary<string, object> Map { get; private set; }

        private static BoxingPacker boxingPacker = null;
        private static BoxingPacker BoxingPacker
        {
            get
            {
                if (boxingPacker == null)
                    boxingPacker = new BoxingPacker();
                return boxingPacker;
            }
        }

        public MemoryStream Stream
        {
            get { return _Stream; }
            set
            {
                _Stream = value;
                OpenMessagePack();
            }
        }

        private void OpenMessagePack()
        {
            try
            {
                var rawObject = BoxingPacker.Unpack(_Stream);

                var type = rawObject.GetType();
                if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(Dictionary<,>))
                    throw new NoDictionaryFoundException();
                var tmp = rawObject as Dictionary<object, object>;
                foreach (var item in tmp)
                {
                    Map[(string)item.Key] = item.Value;
                    Logger.Instance.Log((string)item.Key + ": " + item.Value);
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.Log("[OpenMessagePack] " + ex);
            }
        }

        public ReceiveMessage(MemoryStream stream)
        {
            Map = new Dictionary<string, object>();
            Stream = stream;
        }

        public ReceiveMessage(Dictionary<string, object> map)
        {
            this.Map = map;
        }

        public string GetString(string key)
        {
            object value;
            if (!Map.TryGetValue(key, out value))
                throw new NoAttributeFoundException(key);
            try
            {
                if (value.GetType().GetGenericTypeDefinition() != (typeof(string)))
                    throw new AttributeBadTypeException(key, value.GetType().GetGenericTypeDefinition());
                return (string)value;
            }
            catch (System.NullReferenceException ex)
            {
                throw new AttributeEmptyException(key);
            }
        }


        public byte GetByte(string key)
        {
            object value;
            try
            {
                if (!Map.TryGetValue(key, out value))
                    throw new NoAttributeFoundException(key);

                if (value.GetType().GetGenericTypeDefinition() != (typeof(byte)))
                    throw new AttributeBadTypeException(key, value.GetType().GetGenericTypeDefinition());
                return (byte)value;
            }
            catch (System.NullReferenceException ex)
            {
                throw new AttributeEmptyException(key);
            }
        }


        public int GetInt(string key)
        {
            object value;
            try
            {
                if (!Map.TryGetValue(key, out value))
                    throw new NoAttributeFoundException(key);

                if (value.GetType().GetGenericTypeDefinition() != (typeof(int)))
                    throw new AttributeBadTypeException(key, value.GetType().GetGenericTypeDefinition());
                return (int)value;
            }
            catch (System.NullReferenceException ex)
            {
                throw new AttributeEmptyException(key);
            }
        }

        public float GetFloat(string key)
        {
            object value;
            try
            {
                if (!Map.TryGetValue(key, out value))
                    throw new NoAttributeFoundException(key);

                if (value.GetType().GetGenericTypeDefinition() != (typeof(float)))
                    throw new AttributeBadTypeException(key, value.GetType().GetGenericTypeDefinition());
                return (float)value;
            }
            catch (System.NullReferenceException ex)
            {
                throw new AttributeEmptyException(key);
            }
        }

        public bool GetBoolean(string key)
        {
            object value;
            try
            {
                if (!Map.TryGetValue(key, out value))
                    throw new NoAttributeFoundException(key);

                if (value.GetType().GetGenericTypeDefinition() != (typeof(bool)))
                    throw new AttributeBadTypeException(key, value.GetType().GetGenericTypeDefinition());
                return (bool)value;
            }
            catch (System.NullReferenceException ex)
            {
                throw new AttributeEmptyException(key);
            }
        }

        public ReceiveMessage GetStruct(string key)
        {
            object value;
            if (!Map.TryGetValue(key, out value))
                throw new NoAttributeFoundException(key);
            try
            {
                if (!value.GetType().IsGenericType || value.GetType().GetGenericTypeDefinition() != typeof(Dictionary<,>))
                    throw new AttributeBadTypeException(key, value.GetType().GetGenericTypeDefinition());
                Dictionary<string, object> ret = new Dictionary<string, object>();
                foreach (var item in value as Dictionary<object, object>)
                {
                    ret[(string)item.Key] = item.Value;
                }
                return new ReceiveMessage(ret);
            }
            catch (System.NullReferenceException ex)
            {
                throw new AttributeEmptyException(key);
            }
        }


        public string GetMessageType()
        {
            return GetString("type");
        }

        public string GetMessageId()
        {
            return GetString("id");
        }

    }
}