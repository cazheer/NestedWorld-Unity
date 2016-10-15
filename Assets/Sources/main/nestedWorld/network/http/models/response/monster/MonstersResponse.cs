using System;
using System.Collections.Generic;
using nestedWorld.models;

namespace nestedWorld.network.http.models.reponse.monster
{
    [Serializable]
    public class MonstersResponse
    {
        public List<Monster> monsters;
    }
}
