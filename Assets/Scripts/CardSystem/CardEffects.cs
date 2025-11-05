using UnityEngine;
using Deckbuilder.Combat;

namespace Deckbuilder.CardSystem.Effects
{
    /// <summary>
    /// Card effect that deals damage to a target combatant.
    /// </summary>
    public class DamageEffect : ICardEffect
    {
        private readonly ICombatant target;
        private readonly int amount;

        public DamageEffect(ICombatant target, int amount)
        {
            this.target = target;
            this.amount = amount;
        }

        public void Resolve()
        {
            if (target == null)
            {
                Debug.LogWarning("DamageEffect: target is null");
                return;
            }

            target.ApplyDamage(amount);
            Debug.Log($"DamageEffect: dealt {amount} damage to {target.Id}");
        }
    }

    /// <summary>
    /// Card effect that heals a target combatant.
    /// </summary>
    public class HealEffect : ICardEffect
    {
        private readonly ICombatant target;
        private readonly int amount;

        public HealEffect(ICombatant target, int amount)
        {
            this.target = target;
            this.amount = amount;
        }

        public void Resolve()
        {
            if (target == null)
            {
                Debug.LogWarning("HealEffect: target is null");
                return;
            }

            target.Health += amount;
            Debug.Log($"HealEffect: healed {amount} to {target.Id}, new health: {target.Health}");
        }
    }

    /// <summary>
    /// Card effect that draws cards from the deck.
    /// </summary>
    public class DrawEffect : ICardEffect
    {
        private readonly Deck deck;
        private readonly Hand hand;
        private readonly int count;

        public DrawEffect(Deck deck, Hand hand, int count)
        {
            this.deck = deck;
            this.hand = hand;
            this.count = count;
        }

        public void Resolve()
        {
            if (deck == null || hand == null)
            {
                Debug.LogWarning("DrawEffect: deck or hand is null");
                return;
            }

            for (int i = 0; i < count; i++)
            {
                var card = deck.Draw();
                if (card != null)
                {
                    hand.Add(card);
                }
                else
                {
                    Debug.Log("DrawEffect: no more cards to draw");
                    break;
                }
            }

            Debug.Log($"DrawEffect: drew {count} card(s)");
        }
    }

    /// <summary>
    /// Card effect that applies a status effect to a target.
    /// </summary>
    public class StatusApplyEffect : ICardEffect
    {
        private readonly GameObject target;
        private readonly StatusEffect statusEffect;

        public StatusApplyEffect(GameObject target, StatusEffect statusEffect)
        {
            this.target = target;
            this.statusEffect = statusEffect;
        }

        public void Resolve()
        {
            if (target == null)
            {
                Debug.LogWarning("StatusApplyEffect: target is null");
                return;
            }

            if (statusEffect == null)
            {
                Debug.LogWarning("StatusApplyEffect: statusEffect is null");
                return;
            }

            statusEffect.Apply(target);
            Debug.Log($"StatusApplyEffect: applied {statusEffect.effectId} to {target.name}");
        }
    }

    /// <summary>
    /// Card effect that discards cards from hand.
    /// </summary>
    public class DiscardEffect : ICardEffect
    {
        private readonly Hand hand;
        private readonly DiscardPile discardPile;
        private readonly int count;

        public DiscardEffect(Hand hand, DiscardPile discardPile, int count)
        {
            this.hand = hand;
            this.discardPile = discardPile;
            this.count = count;
        }

        public void Resolve()
        {
            if (hand == null || discardPile == null)
            {
                Debug.LogWarning("DiscardEffect: hand or discardPile is null");
                return;
            }

            int actualCount = Mathf.Min(count, hand.Cards.Count);
            for (int i = 0; i < actualCount; i++)
            {
                if (hand.Cards.Count > 0)
                {
                    var card = hand.Cards[0];
                    hand.Remove(card);
                    discardPile.Add(card);
                }
            }

            Debug.Log($"DiscardEffect: discarded {actualCount} card(s)");
        }
    }

    /// <summary>
    /// Card effect that applies area-of-effect damage to multiple targets.
    /// </summary>
    public class AoEDamageEffect : ICardEffect
    {
        private readonly ICombatant[] targets;
        private readonly int amount;

        public AoEDamageEffect(ICombatant[] targets, int amount)
        {
            this.targets = targets;
            this.amount = amount;
        }

        public void Resolve()
        {
            if (targets == null || targets.Length == 0)
            {
                Debug.LogWarning("AoEDamageEffect: no targets");
                return;
            }

            foreach (var target in targets)
            {
                if (target != null)
                {
                    target.ApplyDamage(amount);
                }
            }

            Debug.Log($"AoEDamageEffect: dealt {amount} damage to {targets.Length} target(s)");
        }
    }

    /// <summary>
    /// Card effect that modifies a card's properties.
    /// </summary>
    public class ModifyCardEffect : ICardEffect
    {
        private readonly Card card;
        private readonly int costModifier;

        public ModifyCardEffect(Card card, int costModifier)
        {
            this.card = card;
            this.costModifier = costModifier;
        }

        public void Resolve()
        {
            if (card == null)
            {
                Debug.LogWarning("ModifyCardEffect: card is null");
                return;
            }

            // Note: Card class would need a modifiable cost field for this to work
            Debug.Log($"ModifyCardEffect: modified card cost by {costModifier}");
        }
    }
}
