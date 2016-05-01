using UnityEngine;
using System.Collections;

[System.Serializable]
public class UserEntity
{
    public bool isActive { get; set; }
    public string registeredAt { get; set; }
    public string city { get; set; }
    public string gender { get; set; }
    public string birthDate { get; set; }
    public string email { get; set; }
    public string pseudo { get; set; }
}
