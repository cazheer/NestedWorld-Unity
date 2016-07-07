using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserMenu : MonoBehaviour
{
    [SerializeField]
    private RawImage image;
    [SerializeField]
    private Text charaName;
    [SerializeField]
    private Text email;

    [SerializeField]
    private GameObject template;

    private GameObject menuContainer;
    private UserData userData;

    void Start()
    {
        Logger.Instance.Init(GetComponent<Canvas>());

        menuContainer = GameObject.FindGameObjectWithTag("MenuContainer");
        transform.SetParent(menuContainer.transform);

        userData = GameObject.FindGameObjectWithTag("Requester").GetComponent<UserData>();

        UpdateUser();
    }

    void OnEnable()
    {
        Logger.Instance.Init(GetComponent<Canvas>());

        if (userData != null)
            UpdateUser();
    }

    void UpdateUser()
    {
        // update user data
        charaName.text = userData.user.pseudo;
        email.text = userData.email;

        // launch update list
        UpdateMonsterList();
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

    public void Return()
    {
        Transform home = menuContainer.transform.FindChild("HomeMenu");
        home.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
