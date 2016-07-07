using Network.MessagePack;

namespace MessagePack.Serveur.Combat
{
    public class End : ResultBase
    {
        public string Status { get; set; }
        public Struct.Stats Stats { get; set; }

        public End()
            : base("combat:end")
        { }
        public override void SetValue(ReceiveMessage receiveMessage)
        {
            try
            {
                Status = receiveMessage.GetString("status");
                var tmp = receiveMessage.GetStruct("stats");
                Stats = new Struct.Stats()
                {
                    Id = tmp.GetInt("id"),
                    Exp = tmp.GetInt("exp"),
                    Level = tmp.GetInt("level"),
                };
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
