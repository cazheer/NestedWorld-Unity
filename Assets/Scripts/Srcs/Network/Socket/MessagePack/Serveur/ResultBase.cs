using Network.MessagePack;

namespace MessagePack.Serveur
{
    public abstract class ResultBase : MessageBase
    {
      
        public delegate void OnCompledCallBack(object value);

        public event OnCompledCallBack OnCompled;

        public string result;

        public ResultBase()
            : base() { }

        public ResultBase(string type)
           : base(type, "") { }


        public abstract void SetValue(ReceiveMessage receiveMessage);

        protected void onCompled()
        {
            if (OnCompled != null)
                OnCompled.Invoke(this);
        }

        public void OnError(System.Exception ex)
        {
            Logger.Instance.Log("[" + this.GetType().ToString() + "] " + ex);
        }

        public void OnError(string kind)
        {
            Logger.Instance.Log("[Receive Message] " + kind);
        }

        public void OnError(string kind, string message)
        {
            Logger.Instance.Log("[Resutl] " + kind + " : " + message);
        }
    }
}
