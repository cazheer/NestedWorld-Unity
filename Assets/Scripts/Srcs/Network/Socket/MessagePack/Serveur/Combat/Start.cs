using Network.MessagePack;

namespace MessagePack.Serveur.Combat
{
    public class Start : ResultBase
    {
        public int combat_id { get; set; }

        public Struct.Monster UserMonster { get; set; }
        #region Oppement

        public Struct.User OppomentUser { get; set; }
        public Struct.Monster OppomentMonster { get; set; }
        public int OppomentMonstersCount { get; set; }

        #endregion
        public string combat_type { get; set; }
        public string combat_env { get; set; }
        public bool first { get; set; }

        public Start()
            : base("combat:start")
        {

        }

        public override void SetValue(ReceiveMessage receiveMessage)
        {

            try
            {
                combat_id = receiveMessage.GetInt("id");
                combat_type = receiveMessage.GetString("type");
                combat_env = receiveMessage.GetString("env");
                first = receiveMessage.GetBoolean("first");

                var tmpUserMonster = receiveMessage.GetStruct("user").GetStruct("monster");

                UserMonster = new Struct.Monster()
                {
                    Id = tmpUserMonster.GetInt("id"),
                    Name = tmpUserMonster.GetString("name"),
                    Monster_Id = tmpUserMonster.GetInt("monster_id"),
                    user_monster_id = tmpUserMonster.GetInt("user_monster_id"),
                    Hp = tmpUserMonster.GetInt("hp"),
                    Level = tmpUserMonster.GetInt("level"),
                };

                var tmpOppUser = receiveMessage.GetStruct("oppoment").GetStruct("user");
                OppomentUser = new Struct.User()
                {
                    Id = tmpOppUser.GetInt("id"),
                    Name = tmpOppUser.GetString("name"),
                };

                var tmpOppMonster = receiveMessage.GetStruct("oppoment").GetStruct("monster");

                OppomentMonster = new Struct.Monster()
                {
                    Id = tmpOppMonster.GetInt("id"),
                    Name = tmpOppMonster.GetString("name"),
                    Monster_Id = tmpOppMonster.GetInt("monster_id"),
                    user_monster_id = -1,
                    Hp = tmpOppMonster.GetInt("hp"),
                    Level = tmpOppMonster.GetInt("level"),
                };

                OppomentMonstersCount = receiveMessage.GetStruct("oppoment").GetInt("monsters_count");

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
