using System.Collections.Generic;

namespace nestedWorld.models
{
    public class DataContainer
    {
        private static DataContainer _instance;

        public static DataContainer Instance
        {
            get { return _instance ?? (_instance = new DataContainer()); }
        }

        public Token Token;
        public User Self;
        public List<UserMonster> UserMonsters;
        public List<Friend> Friends;
    }
}
