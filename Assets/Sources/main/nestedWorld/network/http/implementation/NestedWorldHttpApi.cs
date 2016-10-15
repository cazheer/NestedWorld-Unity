using nestedWorld.logs;
using nestedWorld.models;
using nestedWorld.network.http.callback;
using nestedWorld.network.http.models.request.auth;
using nestedWorld.network.http.models.request.user;
using UnityEngine;

namespace nestedWorld.network.http.implementation
{
    public class NestedWorldHttpApi : HttpApi
    {
        private const string AppToken = "test";

        public void LogIn(Callback callback,
        string email, string password, string data = "")
        {
            ConsoleLogger.Instance.Log("[Call] LogIn");

            var url = HttpEndPoint.BASE_END_POINT + HttpEndPoint.LOGIN;

            var json = JsonUtility.ToJson(new LogInRequest()
            {
                app_token = AppToken,
                data = data,
                email = email,
                password = password
            });

            Post(url, callback, null, json);
        }

        public void LogOut(Callback callback, Token token)
        {
            ConsoleLogger.Instance.Log("[Call] LogOut");

            var url = HttpEndPoint.BASE_END_POINT + HttpEndPoint.LOGOUT;

            var json = JsonUtility.ToJson(new LogOutRequest()
            {

            });

            Post(url, callback, token, json);
        }

        public void Register(Callback callback,
            string pseudo, string email, string password)
        {
            ConsoleLogger.Instance.Log("[Call] Register");

            var url = HttpEndPoint.BASE_END_POINT + HttpEndPoint.REGISTER;

            var json = JsonUtility.ToJson(new RegisterRequest()
            {
                pseudo = pseudo,
                email = email,
                password = password
            });

            Post(url, callback, null, json);
        }

        public void ResetPassword(Callback callback,
            string email)
        {
            ConsoleLogger.Instance.Log("[Call] ResetPassword");

            var url = HttpEndPoint.BASE_END_POINT + HttpEndPoint.REGISTER;

            var json = JsonUtility.ToJson(new ResetPasswordRequest()
            {
                email = email
            });

            Post(url, callback, null, json);
        }

        public void User(Callback callback, Token token)
        {
            ConsoleLogger.Instance.Log("[Call] User");

            var url = HttpEndPoint.BASE_END_POINT + HttpEndPoint.USERS;

            Get(url, callback, token);
        }

        public void UserById(Callback callback, Token token,
            long userId)
        {
            ConsoleLogger.Instance.Log("[Call] UserById");

            var url = HttpEndPoint.BASE_END_POINT + HttpEndPoint.USERS_ID
                .Replace("{user_id}", userId.ToString());

            Get(url, callback, token);
        }

        public void UserFriends(Callback callback, Token token)
        {
            ConsoleLogger.Instance.Log("[Call] UserFriends");

            var url = HttpEndPoint.BASE_END_POINT + HttpEndPoint.USERS_FRIENDS;

            Get(url, callback, token);
        }

        public void AddUserFriend(Callback callback, Token token,
            string friendName)
        {
            ConsoleLogger.Instance.Log("[Call] AddUserFriend");

            var url = HttpEndPoint.BASE_END_POINT + HttpEndPoint.USERS_FRIENDS;

            var json = JsonUtility.ToJson(new AddFriendRequest()
            {
                pseudo = friendName
            });

            Post(url, callback, token, json);
        }

        public void UserMonsters(Callback callback, Token token)
        {
            ConsoleLogger.Instance.Log("[Call] UserMonsters");

            var url = HttpEndPoint.BASE_END_POINT + HttpEndPoint.USERS_MONSTERS;

            Get(url, callback, token);
        }

        public void UserInventory(Callback callback, Token token)
        {
            ConsoleLogger.Instance.Log("[Call] UserInventory");

            var url = HttpEndPoint.BASE_END_POINT + HttpEndPoint.USERS_INVENTORY;

            Get(url, callback, token);
        }

        public void Monsters(Callback callback)
        {
            ConsoleLogger.Instance.Log("[Call] Monsters");

            var url = HttpEndPoint.BASE_END_POINT + HttpEndPoint.MONSTERS;

            Get(url, callback, null);
        }

        public void MonsterById(Callback callback,
            long monsterId)
        {
            ConsoleLogger.Instance.Log("[Call] MonsterById");

            var url = HttpEndPoint.BASE_END_POINT + HttpEndPoint.MONSTER_ID
                .Replace("{monster_id}", monsterId.ToString());

            Get(url, callback, null);
        }

        public void AttacksByIdMonster(Callback callback,
            long monsterId)
        {
            ConsoleLogger.Instance.Log("[Call] AttacksByIdMonster");

            var url = HttpEndPoint.BASE_END_POINT + HttpEndPoint.MONSTER_ID_ATTACKS
                .Replace("{monster_id}", monsterId.ToString());

            Get(url, callback, null);
        }

        public void Objects(Callback callback)
        {
            ConsoleLogger.Instance.Log("[Call] Objects");

            var url = HttpEndPoint.BASE_END_POINT + HttpEndPoint.OBJECTS;

            Get(url, callback, null);
        }

        public void Attacks(Callback callback)
        {
            ConsoleLogger.Instance.Log("[Call] Attacks");

            var url = HttpEndPoint.BASE_END_POINT + HttpEndPoint.ATTACKS;

            Get(url, callback, null);
        }

        public void AttackById(Callback callback,
            long attackId)
        {
            ConsoleLogger.Instance.Log("[Call] AttackById");

            var url = HttpEndPoint.BASE_END_POINT + HttpEndPoint.ATTACK_ID
                          .Replace("{attack_id}", attackId.ToString());

            Get(url, callback, null);
        }
    }
}
