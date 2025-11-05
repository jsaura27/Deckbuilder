using System;
using UnityEngine;

namespace Deckbuilder.Combat
{
    /// <summary>
    /// Status effect that freezes a target, preventing actions.
    /// </summary>
    [CreateAssetMenu(menuName = "Combat/Effects/Freeze")]
    public class FreezeEffect : StatusEffect
    {
        public override void Apply(GameObject target)
        {
            if (target == null)
            {
                Debug.LogWarning("FreezeEffect: target is null");
                return;
            }

            Debug.Log($"FreezeEffect applied to {target.name} for {duration} turn(s)");
            // TODO: Implement actual freeze logic - disable actions/movement for duration
        }
    }

    /// <summary>
    /// Status effect that deals damage over time.
    /// </summary>
    [CreateAssetMenu(menuName = "Combat/Effects/Poison")]
    public class PoisonEffect : StatusEffect
    {
        [SerializeField] private int damagePerTurn = 2;

        public int DamagePerTurn => damagePerTurn;

        public override void Apply(GameObject target)
        {
            if (target == null)
            {
                Debug.LogWarning("PoisonEffect: target is null");
                return;
            }

            Debug.Log($"PoisonEffect applied to {target.name}: {damagePerTurn} damage per turn for {duration} turn(s)");
            // TODO: Register with status effect manager to apply damage each turn
        }
    }

    /// <summary>
    /// Status effect that provides temporary damage absorption.
    /// </summary>
    [CreateAssetMenu(menuName = "Combat/Effects/Shield")]
    public class ShieldEffect : StatusEffect
    {
        [SerializeField] private int shieldAmount = 5;

        public int ShieldAmount => shieldAmount;

        public override void Apply(GameObject target)
        {
            if (target == null)
            {
                Debug.LogWarning("ShieldEffect: target is null");
                return;
            }

            Debug.Log($"ShieldEffect applied to {target.name}: {shieldAmount} shield for {duration} turn(s)");
            // TODO: Add shield value to target's temporary hitpoints
        }
    }

    /// <summary>
    /// Status effect that prevents a target from taking any action for one or more turns.
    /// </summary>
    [CreateAssetMenu(menuName = "Combat/Effects/Stun")]
    public class StunEffect : StatusEffect
    {
        public override void Apply(GameObject target)
        {
            if (target == null)
            {
                Debug.LogWarning("StunEffect: target is null");
                return;
            }

            Debug.Log($"StunEffect applied to {target.name} for {duration} turn(s)");
            // TODO: Disable all actions for the stunned duration
        }
    }

    /// <summary>
    /// Status effect that increases damage taken by the target.
    /// </summary>
    [CreateAssetMenu(menuName = "Combat/Effects/Vulnerable")]
    public class VulnerableEffect : StatusEffect
    {
        [SerializeField] private float damageMultiplier = 1.5f;

        public float DamageMultiplier => damageMultiplier;

        public override void Apply(GameObject target)
        {
            if (target == null)
            {
                Debug.LogWarning("VulnerableEffect: target is null");
                return;
            }

            Debug.Log($"VulnerableEffect applied to {target.name}: {damageMultiplier}x damage for {duration} turn(s)");
            // TODO: Modify damage calculation to apply multiplier when target takes damage
        }
    }

    /// <summary>
    /// Status effect that reduces damage dealt by the target.
    /// </summary>
    [CreateAssetMenu(menuName = "Combat/Effects/Weak")]
    public class WeakEffect : StatusEffect
    {
        [SerializeField] private float damageReduction = 0.75f;

        public float DamageReduction => damageReduction;

        public override void Apply(GameObject target)
        {
            if (target == null)
            {
                Debug.LogWarning("WeakEffect: target is null");
                return;
            }

            Debug.Log($"WeakEffect applied to {target.name}: {damageReduction}x damage dealt for {duration} turn(s)");
            // TODO: Modify damage calculation to apply reduction when target deals damage
        }
    }

    /// <summary>
    /// Status effect that regenerates health over time.
    /// </summary>
    [CreateAssetMenu(menuName = "Combat/Effects/Regeneration")]
    public class RegenerationEffect : StatusEffect
    {
        [SerializeField] private int healPerTurn = 2;

        public int HealPerTurn => healPerTurn;

        public override void Apply(GameObject target)
        {
            if (target == null)
            {
                Debug.LogWarning("RegenerationEffect: target is null");
                return;
            }

            Debug.Log($"RegenerationEffect applied to {target.name}: {healPerTurn} heal per turn for {duration} turn(s)");
            // TODO: Register with status effect manager to apply healing each turn
        }
    }

    /// <summary>
    /// Status effect that reflects a portion of damage back to the attacker.
    /// </summary>
    [CreateAssetMenu(menuName = "Combat/Effects/Thorns")]
    public class ThornsEffect : StatusEffect
    {
        [SerializeField] private int reflectDamage = 3;

        public int ReflectDamage => reflectDamage;

        public override void Apply(GameObject target)
        {
            if (target == null)
            {
                Debug.LogWarning("ThornsEffect: target is null");
                return;
            }

            Debug.Log($"ThornsEffect applied to {target.name}: {reflectDamage} reflected damage for {duration} turn(s)");
            // TODO: Hook into damage calculation to reflect damage to attacker
        }
    }
}
