using MessagePack.Serveur;

namespace MessagePack.Client.Answers
{
    public abstract class AnswerBase
    {
        public delegate void OnCompledCallBack(object value);

        public event OnCompledCallBack OnCompled;


        public AnswerBase()
        {
           
        }


        public abstract void SetValue(ResultRequest result);

        public void OnError(System.Exception ex)
        {
            Logger.Instance.Log("[" + this.GetType().ToString() + "] " + ex);
        }
    }
}
