using System;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder.Tests.Utils
{
    // Minimal, targeted validator for Blessings schema to avoid adding external dependencies.
    public static class JsonSchemaLiteValidator
    {
        private static readonly HashSet<string> ValidRarities = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        { "Common", "Rare", "Epic", "Legendary" };

        public static (bool valid, List<string> errors) ValidateBlessing(JsonUtilityWrapper.BlessingJson bj)
        {
            var errors = new List<string>();
            if (bj == null)
            {
                errors.Add("Parsed object is null");
                return (false, errors);
            }
            if (string.IsNullOrEmpty(bj.id)) errors.Add("Missing required field: id");
            if (string.IsNullOrEmpty(bj.name)) errors.Add("Missing required field: name");
            if (!string.IsNullOrEmpty(bj.rarity) && !ValidRarities.Contains(bj.rarity)) errors.Add($"Invalid rarity: {bj.rarity}");
            return (errors.Count == 0, errors);
        }
    }
}
