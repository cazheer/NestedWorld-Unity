using System;

namespace nestedWorld.models
{
    [Serializable]
    public class User
    {
        public string background;
        public string avatar;
        public string registered_at;
        public bool is_connected;
        public bool is_active;
        public string gender;
        public int level;
        public string email;
        public string birth_date;
        public string city;
        public string pseudo;
    }
}
