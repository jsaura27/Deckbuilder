using System;
using UnityEngine;
using Game.Data;

namespace Deckbuilder.DataModels
{
    /// <summary>
    /// Runtime blessing instance tracking current evolution stage.
    /// </summary>
    [Serializable]
    public class BlessingInstance
    {
        public BlessingDefinition Definition { get; }
        public int CurrentStageIndex { get; set; } = 0;

        public BlessingInstance(BlessingDefinition definition)
        {
            Definition = definition;
        }

        public BlessingEvolutionStage CurrentStage =>
            (Definition != null && Definition.Evolution != null && CurrentStageIndex < Definition.Evolution.Count)
                ? Definition.Evolution[CurrentStageIndex]
                : default;
    }
}