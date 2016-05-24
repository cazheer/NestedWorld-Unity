using UnityEngine;
using System.Collections.Generic;

public class UserData : MonoBehaviour
{
    private static UserData instance = null;

    public string email = null;
    public string token = null;

    // self
    public UserEntity user;
    public List<MonsterEntity> userMonsters = new List<MonsterEntity>();
    public List<ObjectEntity> inventory = new List<ObjectEntity>();

    // general
    public List<ObjectEntity> objects = new List<ObjectEntity>();
    public List<MonsterEntity> monsters = new List<MonsterEntity>();
    public List<UserEntity> friends = new List<UserEntity>();
    public List<AttackEntity> attacks = new List<AttackEntity>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
}
