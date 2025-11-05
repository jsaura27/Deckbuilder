using System;

namespace Deckbuilder.Systems.SkillTree.Effects
{
    [Serializable]
    public abstract class SkillNodeEffect
    {
        public abstract void Apply(object target);
        public abstract void Remove(object target);
    }
}
