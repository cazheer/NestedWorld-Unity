using System;

namespace nestedWorld.models
{
    [Serializable]
    public class UserMonster
    {
        public long id;
        public Monster infos;
        public int level;
        public string surname;
        public int experience;
    }
}
