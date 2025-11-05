using System;
using UnityEngine;

namespace Deckbuilder.DataModels
{
    // Minimal placeholder for polymorphic effects. Start simple: type + JSON payload.
    [CreateAssetMenu(fileName = "NewEffect", menuName = "Game/Effect")]
    public class EffectDefinition : ScriptableObject
    {
        public string effectType = ""; // maps to schema `effects[].effectType`
        [TextArea]
        public string payloadJson = ""; // serialized details; iterate later to typed payloads
    }
}
