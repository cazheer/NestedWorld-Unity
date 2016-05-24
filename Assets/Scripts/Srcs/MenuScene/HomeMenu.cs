using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class HomeMenu : MonoBehaviour
{
    UserData userData;
    HttpRequest request;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

	void Start ()
    {
        MaterialUI.ToastControl.InitToastSystem(GetComponent<Canvas>());

        GameObject requester = GameObject.FindGameObjectWithTag("Requester");
        userData = requester.GetComponent<UserData>();
        request = requester.GetComponent<HttpRequest>();

        // Request user & userMonsters & Inventory & Attacks data
        request.GetUser(UserOnSuccess, UserOnFailure);
        request.GetUserMonsters(UserMonstersOnSuccess, UserMonstersOnFailure);
        request.GetUserInventory(UserInventoryOnSuccess, UserInventoryOnFailure);
        request.GetAttacks(AttacksOnSuccess, AttacksOnFailure);
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

    public void SeekOpponent()
    {
        SceneManager.LoadScene("GameScene");
        gameObject.SetActive(false);
    }

    public void Monsters()
    {

    }

    public void User()
    {

    }

    public void Disconnect()
    {
        request.PostLogOut(LogOutOnSuccess, LogOutOnFailure);
    }

    bool LogOutOnSuccess(JSONNode node)
    {
        Destroy(GameObject.FindGameObjectWithTag("Requester"));
        SceneManager.LoadScene("LoginScene");
        Destroy(gameObject);

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
