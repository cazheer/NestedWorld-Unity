using System;

namespace nestedWorld.models
{
    [Serializable]
    public class Combat
    {
        public string combatId;
        public string type;
        public string origin;
        public long monsterId;
        public string opponentPseudo;
    }
}
