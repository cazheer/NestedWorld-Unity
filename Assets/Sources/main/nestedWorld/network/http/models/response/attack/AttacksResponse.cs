using System;
using System.Collections.Generic;
using nestedWorld.models;

namespace nestedWorld.network.http.models.reponse.attack
{
    [Serializable]
    public class AttacksResponse
    {
        public List<Attack> attacks;
    }
}
