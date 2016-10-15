using System;

namespace nestedWorld.models
{
    [Serializable]
    public class Monster
    {
        public long id;
        public int speed;
        public int attack;
        public int hp;
        public int defense;
        public string type;
        public string name;
    }
}
