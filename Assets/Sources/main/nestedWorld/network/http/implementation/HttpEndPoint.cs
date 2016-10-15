namespace nestedWorld.network.http.implementation
{
    public class HttpEndPoint
    {
        private static string BASE_URL = "http://eip-api.kokakiwi.net/";
        private static string DEV_URL = "http://eip-api-dev.kokakiwi.net/";
        private static string API_VERSION = "v1";
        public static string BASE_END_POINT = DEV_URL + API_VERSION;
        /* 
        **  Auth
        */
        private static string AUTH_PREFIX = "/users/auth/";
    
        public static string LOGIN = AUTH_PREFIX + "login/simple";
        public static string LOGOUT = AUTH_PREFIX + "logout";
        public static string REGISTER = AUTH_PREFIX + "register";
        public static string RESET_PASSWORD = AUTH_PREFIX + "resetpassword";
        /*
        **  Users
        */
        private static string USERS_PREFIX = "/users/";
    
        public static string USERS = USERS_PREFIX;
        public static string USERS_ID = USERS_PREFIX + "{user_id}";
        public static string USERS_FRIENDS = USERS_PREFIX + "friends/";
        public static string USERS_MONSTERS = USERS_PREFIX + "monsters/";
        public static string USERS_INVENTORY = USERS_PREFIX + "inventory/";
        /*
        **  Monsters
        */
        private static string MONSTERS_PREFIX = "/monsters/";
    
        public static string MONSTERS = MONSTERS_PREFIX;
        public static string MONSTER_ID = MONSTERS_PREFIX + "{monster_id}";
        public static string MONSTER_ID_ATTACKS = MONSTERS_PREFIX + "{monster_id}/attacks";
        /*
        **  Attacks
        */
        private static string ATTACK_PREFIX = "/attacks/";

        public static string ATTACKS = ATTACK_PREFIX;
        public static string ATTACK_ID = ATTACK_PREFIX + "{attack_id}";
        /*
        **  Default
        */
        public static string OBJECTS = "/objects/";
    }
}
