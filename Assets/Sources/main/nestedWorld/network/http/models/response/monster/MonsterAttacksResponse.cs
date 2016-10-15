using System;
using System.Collections.Generic;
using nestedWorld.models;

namespace nestedWorld.network.http.models.reponse.monster
{
    [Serializable]
    public class MonsterAttacksResponse
    {
        public List<AttackWithId> attacks;

        [Serializable]
        public class AttackWithId
        {
            public long id;
            public Attack infos;
        }
    }
}
