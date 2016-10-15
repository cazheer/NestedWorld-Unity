using System;
using System.Collections.Generic;
using nestedWorld.models;

namespace nestedWorld.network.http.models.user
{
    [Serializable]
    public class UserMonstersResponse
    {
        public List<UserMonster> monsters;
    }
}
