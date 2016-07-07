using Network.MessagePack;

namespace MessagePack.Serveur.Combat
{
    public class AttackReceived : ResultBase
    {
        public int Attack { get; set; }

        public Struct.AttackMonster Monster { get; set; }
        public Struct.AttackMonster Target { get; set; }

        public AttackReceived() : base("combat:attack-received") { }

        public override void SetValue(ReceiveMessage receiveMessage)
        {
            try
            {
                Attack = receiveMessage.GetInt("attack");
                var monstertmp = receiveMessage.GetStruct("monster");
                Monster = new Struct.AttackMonster() { Hp = monstertmp.GetInt("hp"), Id = monstertmp.GetInt("id")};

                var targettmp = receiveMessage.GetStruct("target");
                Target = new Struct.AttackMonster() { Hp = targettmp.GetInt("hp"), Id = targettmp.GetInt("id") };

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
