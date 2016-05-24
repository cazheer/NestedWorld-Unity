using UnityEngine;
using System.Collections;
using SimpleJSON;

[System.Serializable]
public class ObjectEntity
{
    public bool premium { get; set; }
    public int id { get; set; }
    public int price { get; set; }
    public string name { get; set; }

    public ObjectEntity(bool _premium = false, int _id = 0, int _price = 0, string _name = "")
    {
        premium = _premium;
        id = _id;
        price = _price;
        name = _name;
    }

    public ObjectEntity(JSONClass node)
    {
        if (node["premium"] == null || node["id"] == null || node["price"] == null || node["name"] == null)
            throw new MissingComponentException();

        premium = node["premium"].AsBool;
        id = node["id"].AsInt;
        price = node["price"].AsInt;
        name = node["name"].Value;
    }
}
