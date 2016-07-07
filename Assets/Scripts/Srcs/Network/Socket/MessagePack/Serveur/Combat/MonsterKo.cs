using Network.MessagePack;

namespace MessagePack.Serveur.Combat
{
    public class MonsterKo : ResultBase
    {
        public int Monster { get; set; }

        public MonsterKo() : base("combat:monster-ko") { }
        public override void SetValue(ReceiveMessage receiveMessage)
        {
            try
            {
                Monster = receiveMessage.GetInt("monster");
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
