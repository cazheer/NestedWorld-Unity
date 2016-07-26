using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CombatPopup : MonoBehaviour
{
    ServerListener server;

    [SerializeField]
    Text text;
    static Text Text;

    static CombatPopup self;

    void Start()
    {
        Text = text;
        self = this;

        server = GameObject.FindGameObjectWithTag("Requester").GetComponent<ServerListener>();
        var request = new MessagePack.Serveur.Combat.Available();
        server.serverMsg.Get(request.type).OnCompled += AvailableHandler;
        gameObject.SetActive(false);
    }

    static void AvailableHandler(object o)
    {
        var result = o as MessagePack.Serveur.Combat.Available;
        self.gameObject.SetActive(true);
        string t;
        if (result.user != null)
        {
            t = "DUEL FROM " + result.user.Name;
        }
        else
            t = "MONSTER ATTACK: " + result.monster_id;

        Text.text = t;
    }

    public void Accept()
    {
        SceneManager.LoadScene("GameScene");
        var container = GameObject.FindGameObjectWithTag("MenuContainer");
        for (int i = 0; i < container.transform.childCount; ++i)
        {
            var menu = container.transform.GetChild(i).gameObject;
            menu.SetActive(false);
        }
    }

    public void Decline()
    {
        var request = new MessagePack.Client.Answers.Combat.AvailableAnswer();
        request.accept = false;
        server.clientMsg.SendRequest(request);
    }
}
