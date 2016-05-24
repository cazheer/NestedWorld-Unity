using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;

[System.Serializable]
public class MonsterEntity
{
    public int id { get; set; }
    public string name { get; set; }
    public int hp { get; set; }
    public int attack { get; set; }
    public int defense { get; set; }
    public int speed { get; set; }
    public string type { get; set; }
    public string url { get; set; }
    public string level { get; set; }
    public string surname { get; set; }
    public string experience { get; set; }
    public List<int> attacksId = new List<int>();

    public MonsterEntity(int _id = 0, string _name = "",
        int _hp = 0, int _attack = 0, int _defense = 0, int _speed = 0,
        string _type = "")
    {
        id = _id;
        name = _name;
        hp = _hp;
        attack = _attack;
        defense = _defense;
        speed = _speed;
        type = _type;
    }

    public MonsterEntity(JSONClass node)
    {
        if (node["level"] != null)
            level = node["level"].Value;
        if (node["surname"] != null)
            surname = node["surname"].Value;
        if (node["experience"] != null)
            experience = node["experience"].Value;

        if (node["infos"] == null)
            throw new MissingComponentException();
        var infos = node["infos"].AsObject;

        if (infos["id"] == null || infos["name"] == null || infos["hp"] == null ||
            infos["attack"] == null || infos["defense"] == null)
            throw new MissingComponentException();

        id = infos["id"].AsInt;
        name = infos["name"].Value;
        hp = infos["hp"].AsInt;
        attack = infos["attack"].AsInt;
        defense = infos["defense"].AsInt;
        if (infos["speed"] != null)
            speed = infos["speed"].AsInt;
        if (infos["type"] != null)
            type = infos["type"].Value;
    }
}
