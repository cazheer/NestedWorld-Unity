﻿using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class HomeMenu : MonoBehaviour
{
    UserData userData;
    HttpRequest request;

    void Awake()
    {
        DontDestroyOnLoad(gameObject.transform.parent.gameObject);
    }

	void Start()
    {
        MaterialUI.ToastControl.InitToastSystem(GetComponent<Canvas>());

        GameObject requester = GameObject.FindGameObjectWithTag("Requester");
        userData = requester.GetComponent<UserData>();
        request = requester.GetComponent<HttpRequest>();

        UpdateUserInfo();
	}

    public void UpdateUserInfo()
    {
        // Request user & userMonsters & Inventory & Attacks data
        request.GetUser(UserOnSuccess, UserOnFailure);
        request.GetUserMonsters(UserMonstersOnSuccess, UserMonstersOnFailure);
        request.GetUserInventory(UserInventoryOnSuccess, UserInventoryOnFailure);
        request.GetAttacks(AttacksOnSuccess, AttacksOnFailure);
    }

    public void SeekOpponent()
    {
        SceneManager.LoadScene("GameScene");
        gameObject.SetActive(false);
    }

    public void User()
    {
        UpdateUserInfo();
        gameObject.SetActive(false);

        Transform menu = transform.parent.FindChild("UserMenu");
        if (menu != null)
            menu.gameObject.SetActive(true);
        else
            SceneManager.LoadScene("UserScene");
    }

    public void Options()
    {
        gameObject.SetActive(false);

        Transform menu = transform.parent.FindChild("OptionMenu");
        if (menu != null)
            menu.gameObject.SetActive(true);
        else
            SceneManager.LoadScene("OptionScene");
    }

    public void Disconnect()
    {
        request.PostLogOut(LogOutOnSuccess, LogOutOnFailure);
    }

    bool UserOnSuccess(JSONNode node)
    {
        try
        {
            if (node["user"] != null)
                userData.user = new UserEntity(node["user"].AsObject);
        }
        catch (MissingComponentException e)
        {
            Debug.LogError(e.Message);
            return false;
        }
        return true;
    }

    bool UserOnFailure(WWW error)
    {
        JSONNode node = JSON.Parse(error.text);

        string errorText = error.error;
        if (node != null && node["message"] != null)
        {
            errorText += " : " + node["message"].Value;
            if (errorText.StartsWith("400"))
            {
                errorText = node["message"].Value;
            }
        }
        MaterialUI.ToastControl.MakeToast(errorText, 5.0f, Color.white, Color.black, 32);
        return true;
    }

    bool UserMonstersOnSuccess(JSONNode node)
    {
        try
        {
            if (node["monsters"] != null)
            {
                var monsters = node["monsters"].Childs;
                foreach (var monster in monsters)
                {
                    userData.userMonsters.Add(new MonsterEntity(monster.AsObject));
                }
            }
        }
        catch (MissingComponentException e)
        {
            Debug.LogError(e.Message);
            return false;
        }
        return true;
    }

    bool UserMonstersOnFailure(WWW error)
    {
        JSONNode node = JSON.Parse(error.text);

        string errorText = error.error;
        if (node != null && node["message"] != null)
        {
            errorText += " : " + node["message"].Value;
            if (errorText.StartsWith("400"))
            {
                errorText = node["message"].Value;
            }
        }
        MaterialUI.ToastControl.MakeToast(errorText, 5.0f, Color.white, Color.black, 32);
        return true;
    }

    bool UserInventoryOnSuccess(JSONNode node)
    {
        try
        {
            if (node["inventory"] != null)
            {
                var inventory = node["inventory"].Childs;
                foreach (var obj in inventory)
                {
                    // TODO
                }
            }
        }
        catch (MissingComponentException e)
        {
            Debug.LogError(e.Message);
            return false;
        }
        return true;
    }

    bool UserInventoryOnFailure(WWW error)
    {
        JSONNode node = JSON.Parse(error.text);

        string errorText = error.error;
        if (node != null && node["message"] != null)
        {
            errorText += " : " + node["message"].Value;
            if (errorText.StartsWith("400"))
            {
                errorText = node["message"].Value;
            }
        }
        MaterialUI.ToastControl.MakeToast(errorText, 5.0f, Color.white, Color.black, 32);
        return true;
    }

    bool AttacksOnSuccess(JSONNode node)
    {
        try
        {
            if (node["attacks"] != null)
            {
                var attacks = node["attacks"].Childs;
                foreach (var attack in attacks)
                {
                    // TODO
                }
            }
        }
        catch (MissingComponentException e)
        {
            Debug.LogError(e.Message);
            return false;
        }
        return true;
    }

    bool AttacksOnFailure(WWW error)
    {
        JSONNode node = JSON.Parse(error.text);

        string errorText = error.error;
        if (node != null && node["message"] != null)
        {
            errorText += " : " + node["message"].Value;
            if (errorText.StartsWith("400"))
            {
                errorText = node["message"].Value;
            }
        }
        MaterialUI.ToastControl.MakeToast(errorText, 5.0f, Color.white, Color.black, 32);
        return true;
    }

    bool LogOutOnSuccess(JSONNode node)
    {
        if (PlayerPrefs.HasKey(Constants.emailCache))
        {
            PlayerPrefs.DeleteKey(Constants.emailCache);
            PlayerPrefs.DeleteKey(Constants.passwordCache);
            PlayerPrefs.Save();
        }

        Destroy(GameObject.FindGameObjectWithTag("Requester"));
        SceneManager.LoadScene("LoginScene");
        Destroy(gameObject.transform.parent.gameObject);

        return true;
    }

    bool LogOutOnFailure(WWW error)
    {
        JSONNode node = JSON.Parse(error.text);

        string errorText = error.error;
        if (node != null && node["message"] != null)
        {
            errorText += " : " + node["message"].Value;
            if (errorText.StartsWith("400"))
            {
                errorText = node["message"].Value;
            }
        }
        MaterialUI.ToastControl.MakeToast(errorText, 5.0f, Color.white, Color.black, 32);
        return true;
    }
}