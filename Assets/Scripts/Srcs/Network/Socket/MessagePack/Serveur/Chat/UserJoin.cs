using Network.MessagePack;

namespace MessagePack.Serveur.Chat
{
    public class UserJoin : ResultBase
    {
        public string channel;

        public string userName;
        public int userId;

        public UserJoin()
            : base("chat:user-joined") { }

        public override void SetValue(ReceiveMessage receiveMessage)
        {
            try
            {
                channel = receiveMessage.GetString("channel");
                var tmp = receiveMessage.GetStruct("user");
                userId = tmp.GetInt("id");
                userName = tmp.GetString("name");
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
