using System;
using UnityEngine;

namespace Deckbuilder.Combat
{
    public interface ICombatant
    {
        string Id { get; }
        int Health { get; set; }
        void ApplyDamage(int amount);
    }
}
