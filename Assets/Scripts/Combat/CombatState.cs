using System;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder.Combat
{
    [Serializable]
    public class CombatState : ScriptableObject
    {
        public List<GameObject> participants = new List<GameObject>();
        public int Seed = Environment.TickCount;
    }
}
