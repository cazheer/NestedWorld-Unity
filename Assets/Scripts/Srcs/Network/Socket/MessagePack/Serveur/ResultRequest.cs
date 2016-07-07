using Network.MessagePack;

namespace MessagePack.Serveur
{
    public class ResultRequest : ResultBase
    {
        private const string ERROR = "error";
        private const string SUCCESS = "success";


        public ReceiveMessage Content;

        public ResultRequest()
            : base("result") { }

        public override void SetValue(ReceiveMessage receiveMessage)
        {

            try
            {
                string result = receiveMessage.GetString("result");

                if (result == ERROR)
                    OnError(receiveMessage.GetString("kind"), receiveMessage.GetString("message"));
                id = receiveMessage.GetString("id");
                Content = receiveMessage;
                onCompled();
                return;
            }
            catch (Exception.NoAttributeFoundException ex)
            {
                OnError(ex);
            }
            catch (Exception.AttributeBadTypeException ex)
            {
                OnError(ex);
            }
            catch (Exception.AttributeEmptyException ex)
            {
                OnError(ex);
            }

        }
    }
}
