using System;
using UnityEngine;

namespace Deckbuilder.Combat
{
    [Serializable]
    public abstract class CombatAction : ScriptableObject
    {
        public int priority = 0;
        public abstract void Execute(GameObject source, GameObject target);
    }
}
