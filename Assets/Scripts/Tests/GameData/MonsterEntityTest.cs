using UnityEngine;
using System.Collections;

[IntegrationTest.DynamicTest ("LoginScene")]
[IntegrationTest.SucceedWithAssertions]
public class MonsterEntityTest : MonoBehaviour
{
    public void Start()
    {
        MonsterEntityAssertion();
    }

    public void MonsterEntityAssertion()
    {
        MonsterEntity entity = new MonsterEntity();
        entity.attack = 10;
        entity.defense = 8;
        entity.hp = 50;
        entity.name = "toto";

        IntegrationTest.Assert(entity.attack == 10);
        IntegrationTest.Assert(entity.defense == 8);
        IntegrationTest.Assert(entity.hp == 50);
        IntegrationTest.Assert(entity.name == "toto");

        IntegrationTest.Pass();
    }
}
