using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fight : MonoBehaviour
{
    ServerListener server;

    [SerializeField]
    private Image[] attackQTE;
    public int cursor = 0;

    public Canvas endCanvas;
    public Text endText;

    Color defaultColor = Color.white;
    Color successColor = Color.gray;

    [SerializeField]
    private Image userHp;
    [SerializeField]
    private Image enemyHp;

    public MessagePack.Serveur.Combat.Struct.Monster userMonster;
    public MessagePack.Serveur.Combat.Struct.Monster enemyMonster;

    public int currentUserMonsterHp;
    public int currentEnemyMonsterHp;

    public int enemyMonsters = 0;
    public int remainingEnemyMonsters;

    [HideInInspector]
    public MonsterEntity[] choosenMonsters;

    static Fight self;

    UserData userData;

    void Start()
    {
        self = this;
        server = GameObject.FindGameObjectWithTag("Requester").GetComponent<ServerListener>();
        userData = GameObject.FindGameObjectWithTag("Requester").GetComponent<UserData>();
    }

    public void Launch()
    {
        if (userMonster == null || enemyMonster == null)
            return;

        // here load monster figure

        userMonster.Hp = userMonster.Hp;
        enemyMonster.Hp = enemyMonster.Hp;
        remainingEnemyMonsters = enemyMonsters;

        // Handlers
        var requestAR = new MessagePack.Serveur.Combat.AttackReceived();
        server.serverMsg.Get(requestAR.type).OnCompled += AttackReceivedHandler;
        var requestKO = new MessagePack.Serveur.Combat.MonsterKo();
        server.serverMsg.Get(requestKO.type).OnCompled += MonsterKOHandler;
//        var requestR = new MessagePack.Serveur.Combat.();
//        server.serverMsg.Get(requestR.type).OnCompled += MonsterReplacedHandler;
        var requestE = new MessagePack.Serveur.Combat.End();
        server.serverMsg.Get(requestE.type).OnCompled += CombatEndHandler;
    }

    static void AttackReceivedHandler(object o)
    {
        var r = o as MessagePack.Serveur.Combat.AttackReceived;
        self.userMonster.Hp = r.Monster.Hp;
        self.DecreaseSelfLife(0);
    }

    static void MonsterKOHandler(object o)
    {
        var r = o as MessagePack.Serveur.Combat.MonsterKo;
        self.userMonster.Hp = 0;
        self.DecreaseSelfLife(0);

        // Monster KO
    }

    static void MonsterReplacedHandler(object o)
    {

    }

    static void CombatEndHandler(object o)
    {
        var r = o as MessagePack.Serveur.Combat.End;

        self.endCanvas.gameObject.SetActive(true);
        self.endText.text = r.Status;
    }

    void Update()
    {
        var valueNeeded = attackQTE[cursor].GetComponent<ButtonValueQTE>().value;

        bool left = Input.GetKeyDown(KeyCode.LeftArrow);
        bool right = Input.GetKeyDown(KeyCode.RightArrow);
        bool up = Input.GetKeyDown(KeyCode.UpArrow);
        bool down = Input.GetKeyDown(KeyCode.DownArrow);

        if ((valueNeeded == ButtonValueQTE.ButtonValue.LEFT && left && !right && !up && !down) ||
            (valueNeeded == ButtonValueQTE.ButtonValue.DOWN && !left && !right && !up && down) ||
            (valueNeeded == ButtonValueQTE.ButtonValue.RIGHT && !left && right && !up && !down) ||
            (valueNeeded == ButtonValueQTE.ButtonValue.UP && !left && !right && up && !down))
            Success();
        else if (left || right || up || down)
            Failure();

        if (attackQTE.Length == cursor)
        {
            Attack();
        }
    }

    void Success()
    {
        attackQTE[cursor].color = successColor;
        cursor++;
    }

    void Failure()
    {
        while (cursor > 0)
        {
            cursor--;
            attackQTE[cursor].color = defaultColor;
        }
    }

    public void Attack()
    {
        int attackId = 0;
        foreach (var monster in userData.monsters)
        {
            if (monster.id == userMonster.user_monster_id)
            {
                if (monster.attacksId.Count > 0)
                    attackId = monster.attacksId[0];
                break;
            }
        }

        var request = MessagePack.Client.Combat.SendAttack.Attack(attackId, enemyMonster.Monster_Id);
        server.clientMsg.SendRequest(request);

        Failure();
        // use specific attack
    }

    public void DecreaseSelfLife(int dmg)
    {
        currentUserMonsterHp -= dmg;
        userHp.fillAmount = (float)currentUserMonsterHp / userMonster.Hp;
    }

    public void DecreaseEnemyEnemyLife(int dmg)
    {
        currentEnemyMonsterHp -= dmg;
        enemyHp.fillAmount = (float)currentEnemyMonsterHp / enemyMonster.Hp;
    }
}
