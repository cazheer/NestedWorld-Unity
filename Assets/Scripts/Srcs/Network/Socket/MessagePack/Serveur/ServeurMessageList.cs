using Network.MessagePack;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace MessagePack.Serveur
{
    public class ServeurMessageList
    {
        private Dictionary<string, ResultBase> map;

        public ServeurMessageList()
        {
            map = new Dictionary<string, ResultBase>();
        }

        public void Init()
        {
            Add(new ResultRequest());
            Add(new Chat.MessageReceived());
            Add(new Chat.UserJoin());
            Add(new Chat.UserPart());
            Add(new Combat.Available());
            Add(new Combat.Start());
            Add(new Combat.AttackReceived());
            Add(new Combat.MonsterKo());
            Add(new Combat.End());
        }

        public void Add(ResultBase resutl)
        {
            map[resutl.type] = resutl;
        }

        public void Add(string key, ResultBase resutl)
        {
            map[key] = resutl;
        }

        public ResultBase Get(string type)
        {
            ResultBase value;
            if (map.TryGetValue(type, out value))
                return value;
            throw new Exception.NoTypeFoundException(type);
        }

        public void SelectMessage(byte[] obj)
        {
            if (obj == null || obj.Length == 0)
                return;
            
            Logger.Instance.Log("Obj received: " + obj);
            using (MemoryStream ms = new MemoryStream(obj))
            {
                try
                {
                    ReceiveMessage rm = new ReceiveMessage(ms);
                    string type = rm.GetMessageType();

                    Logger.Instance.Log("SelectMessage, type receive: " + type);
                    var ret = Get(type);

                    ret.SetValue(rm);
                }
                catch (Exception.NoTypeFoundException ex)
                {
                    Logger.Instance.Log("ServeurMessageList.SelectMessage: " + ex);
                }
                catch (Exception.NoDictionaryFoundException ex)
                {
                    Logger.Instance.Log("ServeurMessageList.SelectMessage: " + ex);
                }
                catch (Exception.NoAttributeFoundException ex)
                {
                    Logger.Instance.Log("ServeurMessageList.SelectMessage: " + ex);
                }
            }
        }

        public ResultBase this[string key]
        {
            get
            {
                return Get(key);
            }
            set
            {
                Add(key, value);
            }
        }

    }
}