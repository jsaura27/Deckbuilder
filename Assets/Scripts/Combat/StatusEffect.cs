using System;
using UnityEngine;

namespace Deckbuilder.Combat
{
    [Serializable]
    public abstract class StatusEffect : ScriptableObject
    {
        public string effectId;
        public int duration;

        public abstract void Apply(GameObject target);
    }

    [CreateAssetMenu(menuName = "Combat/Effects/Burn")]
    public class BurnEffect : StatusEffect
    {
        public int damagePerTick = 1;

        public override void Apply(GameObject target)
        {
            // Minimal, non-invasive behavior for compile-time safety
            var healthComp = target.GetComponent<MonoBehaviour>();
            // Real logic to be implemented in later tasks
        }
    }
}
