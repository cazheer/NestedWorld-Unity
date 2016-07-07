using Network.MessagePack;

namespace MessagePack.Serveur.Chat
{
    public class MessageReceived : ResultBase
    {
        public string channel;
        public int userId;
        public string message;

        public MessageReceived()
            : base("chat:message-received") { }

        public override void SetValue(ReceiveMessage receiveMessage)
        {
            try
            {
                channel = receiveMessage.GetString("channel");
                userId = receiveMessage.GetInt("user");
                message = receiveMessage.GetString("message");
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
