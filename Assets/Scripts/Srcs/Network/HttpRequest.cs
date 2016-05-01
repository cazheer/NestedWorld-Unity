using UnityEngine;
using System.Collections;
using System;

using SimpleJSON;

public class HttpRequest : MonoBehaviour
{
    protected static string appToken = "test";

    protected static string BASE_URL = "http://eip-api.kokakiwi.net:80/";
    protected static string API_VERSION = "v1";
    protected static string BASE_END_POINT = BASE_URL + API_VERSION;

    protected static string AUTHORIZATION = "?Authorization=";

    public static string sessionToken = null;

    /*
    **  WWW
    */
    protected void Request(Func<JSONNode, bool> c_Success, Func<WWW, bool> c_Error,
        string url, WWWForm form = null)
    {
        if (form == null)
            form = new WWWForm();
        form.headers["Content-Type"] = "application/json";
        form.headers["Accept"] = "application/json";

        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(c_Success, c_Error, www));
    }

    protected IEnumerator WaitForRequest(Func<JSONNode, bool> c_Success, Func<WWW, bool> c_Error, WWW www)
    {
        yield return www;

        if (www.error == null)
        {
            Debug.LogWarning(www.text);
            JSONNode node = JSON.Parse(www.text);
            if (!c_Success(node))
                Debug.LogWarning("[NETWORK] callback return false");
        }
        else
        {
            c_Error(www);
        }
    }

    /* 
    **  Auth
    */
    protected static string USER_LOGIN = "/users/auth/login/simple";
    protected static string USER_LOGOUT = "/users/auth/logout";
    protected static string USER_REGISTER = "/users/auth/register";
    protected static string USER_RESET_PASSWORD = "/users/auth/register";


    public void PostLogIn(Func<JSONNode, bool> c_Success, Func<WWW, bool> c_Error,
        string email, string password, string data = "")
    {
        string url = BASE_END_POINT + USER_LOGIN;

        WWWForm form = new WWWForm();
        form.AddField("app_token", appToken);
        form.AddField("email", email);
        form.AddField("password", password);
        form.AddField("data", data);

        Request(c_Success, c_Error, url, form);
    }

    public void GetLogOut(Func<JSONNode, bool> c_Success, Func<WWW, bool> c_Error,
        string sessionToken)
    {
        string url = BASE_END_POINT + USER_LOGOUT + AUTHORIZATION + sessionToken;

        Request(c_Success, c_Error, url);
    }

    public void PostRegister(Func<JSONNode, bool> c_Success, Func<WWW, bool> c_Error,
        string pseudo, string email, string password)
    {
        string url = BASE_END_POINT + USER_REGISTER;

        WWWForm form = new WWWForm();
        form.AddField("pseudo", pseudo);
        form.AddField("email", email);
        form.AddField("password", password);

        Request(c_Success, c_Error, url, form);
    }

    public void PostResetPassword(Func<JSONNode, bool> c_Success, Func<WWW, bool> c_Error,
        string email)
    {
        string url = BASE_END_POINT + USER_REGISTER;

        WWWForm form = new WWWForm();
        form.AddField("email", email);

        Request(c_Success, c_Error, url, form);
    }

    /*
    **  Users
    */
    protected static string USERS = "/users/";

    public void GetUser(Func<JSONNode, bool> c_Success, Func<WWW, bool> c_Error,
        string sessionToken)
    {
        string url = BASE_END_POINT + USERS + AUTHORIZATION + sessionToken;

        Request(c_Success, c_Error, url);
    }

    /*
    **  Monsters
    */
    protected static string MONSTERS = "/monsters/";

    public void GetMonsters(Func<JSONNode, bool> c_Success, Func<WWW, bool> c_Error)
    {
        string url = BASE_END_POINT + MONSTERS;

        Request(c_Success, c_Error, url);
    }
}
