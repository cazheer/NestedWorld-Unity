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
        menuContainer = GameObject.FindGameObjectWithTag("MenuContainer");
        transform.SetParent(menuContainer.transform);

        userData = GameObject.FindGameObjectWithTag("Requester").GetComponent<UserData>();

        UpdateMonsterList();
    }

    void OnEnable()
    {
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

    void Return()
    {
        Transform home = menuContainer.transform.FindChild("HomeMenu");
        home.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
