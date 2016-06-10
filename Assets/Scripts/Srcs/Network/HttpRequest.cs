using UnityEngine;
using System.Collections;
using System;

using SimpleJSON;

public class HttpRequest : MonoBehaviour
{
    protected static string appToken = "test";

    protected static string BASE_URL = "http://eip-api.kokakiwi.net/";
    protected static string DEV_URL = "http://eip-api-dev.kokakiwi.net/";
    protected static string API_VERSION = "v1";
    protected static string BASE_END_POINT = DEV_URL + API_VERSION;

    UserData userData = null;
    void Start()
    {
        userData = gameObject.GetComponent<UserData>();
    }

    /*
    **  WWW
    */
    protected void Request(Func<JSONNode, bool> c_Success, Func<WWW, bool> c_Error,
        string url, bool useToken = false, string data = null)
    {
        var headers = new System.Collections.Generic.Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");
        headers.Add("Accept", "application/json");
        if (useToken)
        {
            headers.Add("X-User-Email", userData.email);
            headers.Add("Authorization", "Bearer " + userData.token);
        }

        WWW www;
        if (data != null)
        {
            byte[] postData = System.Text.Encoding.UTF8.GetBytes(data);
            www = new WWW(url, postData, headers);
        }
        else
            www = new WWW(url, null, headers);
        StartCoroutine(WaitForRequest(c_Success, c_Error, www));
    }

    protected IEnumerator WaitForRequest(Func<JSONNode, bool> c_Success, Func<WWW, bool> c_Error, WWW www)
    {
        yield return www;

        if (www.error == null)
        {
            Debug.Log(www.text);
            JSONNode node = JSON.Parse(www.text);
            if (!c_Success(node))
                Debug.LogWarning("[NETWORK] callback return false");
        }
        else
        {
            Debug.LogWarning(www.url);
            c_Error(www);
        }
    }

    /* 
    **  Auth
    */
    protected static string USER_LOGIN = "/users/auth/login/simple";
    protected static string USER_LOGOUT = "/users/auth/logout";
    protected static string USER_REGISTER = "/users/auth/register";
    protected static string USER_RESET_PASSWORD = "/users/auth/resetpassword";


    public void PostLogIn(Func<JSONNode, bool> c_Success, Func<WWW, bool> c_Error,
        string email, string password, string data = "")
    {
        string url = BASE_END_POINT + USER_LOGIN;

        var json = "{ \"email\": \"{email}\", \"app_token\": \"{app_token}\", \"password\": \"{password}\", \"data\": \"{data}\" }";
        json = json
            .Replace("{email}", email)
            .Replace("{app_token}", appToken)
            .Replace("{password}", password)
            .Replace("{data}", data);

        Request(c_Success, c_Error, url, false, json);
    }

    public void PostLogOut(Func<JSONNode, bool> c_Success, Func<WWW, bool> c_Error)
    {
        string url = BASE_END_POINT + USER_LOGOUT;

        var json = "{}";
        Request(c_Success, c_Error, url, true, json);
    }

    public void PostRegister(Func<JSONNode, bool> c_Success, Func<WWW, bool> c_Error,
        string pseudo, string email, string password)
    {
        string url = BASE_END_POINT + USER_REGISTER;

        var json = "{\"pseudo\":\"{pseudo}\",\"email\":\"{email}\",\"password\":\"{password}\"}";
        json = json
            .Replace("{pseudo}", pseudo)
            .Replace("{email}", email)
            .Replace("{password}", password);

        Request(c_Success, c_Error, url, false, json);
    }

    public void PostResetPassword(Func<JSONNode, bool> c_Success, Func<WWW, bool> c_Error,
        string email)
    {
        string url = BASE_END_POINT + USER_REGISTER;

        var json = "{\"email\":\"{email}\"}";
        json = json.Replace("{email}", email);

        Request(c_Success, c_Error, url, false, json);
    }

    /*
    **  Users
    */
    protected static string USERS = "/users/";
    protected static string USERSFRIENDS = "/users/friends/";
    protected static string USERSMONSTERS = "/users/monsters/";
    protected static string USERSINVENTORY = "/users/inventory/";

    public void GetUser(Func<JSONNode, bool> c_Success, Func<WWW, bool> c_Error)
    {
        string url = BASE_END_POINT + USERS;

        Request(c_Success, c_Error, url, true);
    }

    public void GetUserFriends(Func<JSONNode, bool> c_Success, Func<WWW, bool> c_Error)
    {
        string url = BASE_END_POINT + USERSFRIENDS;

        Request(c_Success, c_Error, url, true);
    }

    public void GetUserMonsters(Func<JSONNode, bool> c_Success, Func<WWW, bool> c_Error)
    {
        string url = BASE_END_POINT + USERSMONSTERS;

        Request(c_Success, c_Error, url, true);
    }

    public void GetUserInventory(Func<JSONNode, bool> c_Success, Func<WWW, bool> c_Error)
    {
        string url = BASE_END_POINT + USERSINVENTORY;

        Request(c_Success, c_Error, url, true);
    }

    /*
    **  Monsters
    */
    protected static string MONSTERS = "/monsters/";
    protected static string MONSTERID = "/monsters/{monster_id}";
    protected static string MONSTERIDATTACKS = "/monsters/{monster_id}/attacks";

    public void GetMonsters(Func<JSONNode, bool> c_Success, Func<WWW, bool> c_Error)
    {
        string url = BASE_END_POINT + MONSTERS;

        Request(c_Success, c_Error, url);
    }

    public void GetMonsterId(Func<JSONNode, bool> c_Success, Func<WWW, bool> c_Error, int monsterId)
    {
        string url = BASE_END_POINT + MONSTERID;
        url.Replace("{monster_id}", monsterId.ToString());

        Request(c_Success, c_Error, url);
    }

    public void GetMonsterIdAttacks(Func<JSONNode, bool> c_Success, Func<WWW, bool> c_Error, int monsterId)
    {
        string url = BASE_END_POINT + MONSTERIDATTACKS;
        url.Replace("{monster_id}", monsterId.ToString());

        Request(c_Success, c_Error, url);
    }

    /*
    **  Default
    */

    protected static string OBJECTS = "/objects/";
    protected static string ATTACKS = "/attacks/";

    public void GetObjects(Func<JSONNode, bool> c_Success, Func<WWW, bool> c_Error)
    {
        string url = BASE_END_POINT + OBJECTS;

        Request(c_Success, c_Error, url);
    }

    public void GetAttacks(Func<JSONNode, bool> c_Success, Func<WWW, bool> c_Error)
    {
        string url = BASE_END_POINT + ATTACKS;

        Request(c_Success, c_Error, url);
    }
}
