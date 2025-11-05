using System;
using UnityEngine;

namespace Deckbuilder.Combat
{
    public interface IEnemyAI
    {
        CombatAction ChooseAction(GameObject enemy, CombatState state);
    }

    [CreateAssetMenu(menuName = "Combat/AI/PatternAI")]
    public class PatternAI : ScriptableObject, IEnemyAI
    {
        public CombatAction defaultAction;

        public CombatAction ChooseAction(GameObject enemy, CombatState state)
        {
            return defaultAction;
        }
    }
}
