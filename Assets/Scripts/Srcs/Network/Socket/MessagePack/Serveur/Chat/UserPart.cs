using Network.MessagePack;

namespace MessagePack.Serveur.Chat
{

    public class UserPart : ResultBase
    {
        public string channel;

        public int userId;

        public UserPart()
            : base("chat:user-parted") { }

        public override void SetValue(ReceiveMessage receiveMessage)
        {
            try
            {
                channel = receiveMessage.GetString("channel");
                userId = receiveMessage.GetInt("user");
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
