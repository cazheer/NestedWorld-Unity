using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class ChooseMonsterMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject template;

    [SerializeField]
    private GameObject rotating;

    private int currentMonster = 0;

    [SerializeField]
    private HorizontalScrollSnap scroller;
    private UserData userData;
    private ServerListener server;

    private MonsterEntity[] choosenMonsters = new MonsterEntity[4];

    [SerializeField]
    private Fight f;
    static Fight F;
    static ChooseMonsterMenu self;

    void Start()
    {
        F = f;
        self = this;

        Logger.Instance.Init(GetComponent<Canvas>());

        userData = GameObject.FindGameObjectWithTag("Requester").GetComponent<UserData>();
        server = GameObject.FindGameObjectWithTag("Requester").GetComponent<ServerListener>();
        var request = new MessagePack.Serveur.Combat.Start();
        server.serverMsg.Get(request.type).OnCompled += StartHandler;
        UpdateMonsterList();
    }

    static void StartHandler(object o)
    {
        var msg = o as MessagePack.Serveur.Combat.Start;
        // change display monster here

        F.userMonster = msg.UserMonster;
        F.enemyMonster = msg.OppomentMonster;
        F.enemyMonsters = msg.OppomentMonstersCount;
        F.choosenMonsters = self.choosenMonsters;
        F.Launch();

        self.gameObject.SetActive(false);
    }

    void UpdateMonsterList()
    {
        foreach (var monster in userData.userMonsters)
        {
            var obj = Instantiate(template);
            obj.SetActive(true);

            obj.transform.SetParent(template.transform.parent);
            obj.transform.FindChild("MonsterName").gameObject.GetComponent<Text>().text = monster.name;
        }
    }

    public void PutMonster()
    {
        GameObject holder = null;
        switch (currentMonster)
        {
            case 0:
                holder = rotating.transform.FindChild("FirstMonster").gameObject;
                break;
            case 1:
                holder = rotating.transform.FindChild("SecondMonster").gameObject;
                break;
            case 2:
                holder = rotating.transform.FindChild("ThirdMonster").gameObject;
                break;
            case 3:
                holder = rotating.transform.FindChild("FourthMonster").gameObject;
                break;
            default:
                break;
        }
        if (holder == null || scroller.CurrentScreen() == 0)
            return;
        
        holder.GetComponent<RawImage>().color = Color.blue;
        choosenMonsters[currentMonster] = userData.userMonsters[scroller.CurrentScreen() - 1];

        Debug.LogWarning(userData.userMonsters[scroller.CurrentScreen() - 1].name);

        RotateRight();
    }

    public void RotateLeft()
    {
        currentMonster++;
        currentMonster %= 4;

        rotating.transform.Rotate(0, 0, -90, Space.World);
    }

    public void RotateRight()
    {
        currentMonster += 5;
        currentMonster %= 4;

        rotating.transform.Rotate(0, 0, 90, Space.World);
    }

    public void Reset()
    {
        rotating.transform.Rotate(0, 0, (4 - currentMonster) * 90, Space.World);

        rotating.transform.FindChild("FirstMonster").gameObject.GetComponent<RawImage>().color = Color.black;
        rotating.transform.FindChild("SecondMonster").gameObject.GetComponent<RawImage>().color = Color.black;
        rotating.transform.FindChild("ThirdMonster").gameObject.GetComponent<RawImage>().color = Color.black;
        rotating.transform.FindChild("FourthMonster").gameObject.GetComponent<RawImage>().color = Color.black;

        for (var i = 0; i < 4; ++i)
        {
            choosenMonsters[i] = null;
        }
    }

    public void Confirm()
    {
        var count = 0;
        for (var i = 0; i < 4; ++i)
        {
            if (choosenMonsters[i] != null)
                count++;
        }

        if (count > 0)
        {
            var request = new MessagePack.Client.Answers.Combat.AvailableAnswer();
            request.accept = true;
            for (int i = 0; i < 4; ++i)
            {
                if (choosenMonsters[i] != null)
                    request.monsters[i] = choosenMonsters[i].id;
            }
            server.clientMsg.SendRequest(request);

        }
        else
            Logger.Instance.Log("You have to choose at least one monster before proceeding.");
    }

    public void Cancel()
    {
        // TODO check

        var request = new MessagePack.Client.Answers.Combat.AvailableAnswer();
        request.accept = false;
        server.clientMsg.SendRequest(request);

        GameObject.FindGameObjectWithTag("MenuContainer").transform.FindChild("HomeMenu").gameObject.SetActive(true);
        UnityEngine.SceneManagement.SceneManager.UnloadScene("GameScene");
    }
}
