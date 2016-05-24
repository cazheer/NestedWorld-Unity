using UnityEngine;
using System.Collections;
using SimpleJSON;

[System.Serializable]
public class UserEntity
{
    public string email { get; set; }
    public string pseudo { get; set; }
    public bool isActive { get; set; }
    public string registeredAt { get; set; }
    public string city { get; set; }
    public string gender { get; set; }
    public string birthDate { get; set; }
    public string background { get; set; }
    public string avatar { get; set; }

    public UserEntity(string _email = "", string _pseudo = "", bool _isActive = false, string _registeredAt = "",
        string _city = "", string _gender = "", string _birthDate = "", string _background = "", string _avatar = "")
    {
        email = _email;
        pseudo = _pseudo;
        isActive = _isActive;
        registeredAt = _registeredAt;
        city = _city;
        gender = _gender;
        birthDate = _birthDate;
        background = _background;
        avatar = _avatar;
    }

    public UserEntity(JSONClass node)
    {
        if (node["email"] == null || node["pseudo"] == null || node["is_active"] == null || node["registered_at"] == null ||
            node["city"] == null || node["gender"] == null || node["birth_date"] == null || node["background"] == null || node["avatar"] == null)
            throw new MissingComponentException();

        email = node["email"].Value;
        pseudo = node["pseudo"].Value;
        isActive = node["is_active"].AsBool;
        registeredAt = node["registered_at"].Value;
        city = node["city"].Value;
        gender = node["gender"].Value;
        birthDate = node["birth_date"].Value;
        background = node["background"].Value;
        avatar = node["avatar"].Value;
    }
}
