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

    void Start()
    {
        userMonster.hp = 100;
        enemyMonster.hp = 100;
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
        enemyHp.fillAmount = (float)currentEnemyMonsterHp / enemyMonster.hp;
    }
}
