using UnityEngine;
using System.Collections;

[IntegrationTest.DynamicTest("LoginScene")]
[IntegrationTest.SucceedWithAssertions]
public class UserEntityTest : MonoBehaviour
{
    public void Start()
    {
        UserEntityAssertion();
    }

    public void UserEntityAssertion()
    {
        UserEntity entity = new UserEntity();
        entity.isActive = true;
        entity.registeredAt = "02032015T000000Z";
        entity.city = null;
        entity.gender = null;
        entity.birthDate = null;
        entity.email = "toto@toto";
        entity.pseudo = "toto";

        IntegrationTest.Assert(entity.isActive == true);
        IntegrationTest.Assert(entity.registeredAt == "02032015T000000Z");
        IntegrationTest.Assert(entity.city == null);
        IntegrationTest.Assert(entity.gender == null);
        IntegrationTest.Assert(entity.birthDate == null);
        IntegrationTest.Assert(entity.email == "toto@toto");
        IntegrationTest.Assert(entity.pseudo == "toto");

        IntegrationTest.Pass();
    }
}
