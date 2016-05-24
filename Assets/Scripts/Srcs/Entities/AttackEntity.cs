using UnityEngine;
using System.Collections;
using SimpleJSON;

[System.Serializable]
public class AttackEntity
{
    public string name { get; set; }
    public string type { get; set; }
    public int id { get; set; }

    public AttackEntity(string _name = "", string _type = "", int _id = 0)
    {
        name = _name;
        type = _type;
        id = _id;
    }

    public AttackEntity(JSONClass node)
    {
        if (node["name"] == null || node["type"] == null || node["id"] == null)
            throw new MissingComponentException();

        name = node["name"].Value;
        type = node["type"].Value;
        id = node["id"].AsInt;
    }
}
