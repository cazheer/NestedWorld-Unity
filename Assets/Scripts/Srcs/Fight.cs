using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fight : MonoBehaviour
{
    [SerializeField]
    private Image[] attackQTE;
    public int cursor = 0;

    Color defaultColor = Color.white;
    Color successColor = Color.gray;

    [SerializeField]
    private Image userHp;
    [SerializeField]
    private Image enemyHp;

    public MonsterEntity userMonster = new MonsterEntity();
    public MonsterEntity enemyMonster = new MonsterEntity();

    public int currentUserMonsterHp = 100;
    public int currentEnemyMonsterHp = 100;

    private float currentTime = 0f;
    private float MaxTime = 0.2f;

    void Start()
    {
        userMonster.hp = 100;
        enemyMonster.hp = 100;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime < MaxTime)
            return;

        int angle = (int)attackQTE[cursor].transform.rotation.z;

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if ((angle == 0 && x < 0f && y == 0f) ||
            (angle == 90 && x == 0f && y < 0f) ||
            (angle == 180 && x > 0f && y == 0f) ||
            ((angle + 360) % 360 == 270 && x == 0f && y > 0f))
            Success();
        else if (x != 0f || y != 0f)
            Failure();

        if (x != 0f || y != 0f)
            currentTime = 0f;

        if (cursor == attackQTE.Length)
            Attack();
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

    void Attack()
    {
        DecreaseEnemyEnemyLife(20);
        Failure();
        // use specific attack
    }

    void DecreaseSelfLife(int dmg)
    {
        currentUserMonsterHp -= dmg;
        userHp.fillAmount = currentUserMonsterHp / userMonster.hp;
    }

    void DecreaseEnemyEnemyLife(int dmg)
    {
        currentEnemyMonsterHp -= dmg;
        enemyHp.fillAmount = currentEnemyMonsterHp / enemyMonster.hp;
    }
}
