namespace MessagePack.Serveur.Combat.Struct
{
    public class Monster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Monster_Id { get; set; }
        public int user_monster_id { get; set; }
        public int Hp { get; set; }
        public int Level { get; set; }
    }
}
