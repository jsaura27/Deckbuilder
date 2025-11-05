using NUnit.Framework;
using Deckbuilder.DataModels;
using Deckbuilder.Systems;
using Game.Data;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class BlessingManagerTests
{
    private BlessingDefinition CreateTestBlessingDefinition(string id, string displayName, int evolutionStages = 1)
    {
        var def = ScriptableObject.CreateInstance<BlessingDefinition>();
        var serializedObject = new SerializedObject(def);
        
        // Set id
        serializedObject.FindProperty("id").stringValue = id;
        serializedObject.FindProperty("displayName").stringValue = displayName;
        serializedObject.FindProperty("description").stringValue = "Test blessing";
        
        // Add evolution stages if needed
        if (evolutionStages > 0)
        {
            var evolutionProp = serializedObject.FindProperty("evolution");
            evolutionProp.arraySize = evolutionStages;
            
            for (int i = 0; i < evolutionStages; i++)
            {
                var stageProp = evolutionProp.GetArrayElementAtIndex(i);
                stageProp.FindPropertyRelative("stage").intValue = i;
                stageProp.FindPropertyRelative("condition").stringValue = i == 0 ? "" : "ALWAYS";
            }
        }
        
        serializedObject.ApplyModifiedProperties();
        return def;
    }

    [Test]
    public void AcquireBlessing_CreatesInstance()
    {
        var go = new GameObject("BlessingManager");
        var mgr = go.AddComponent<BlessingManager>();
        var def = CreateTestBlessingDefinition("bless_1", "Test Blessing");
        mgr.RegisterDefinition(def);

        var inst = mgr.AcquireBlessing("bless_1");
        Assert.IsNotNull(inst);
        Assert.AreEqual(def, inst.Definition);
        
        Object.DestroyImmediate(go);
    }

    [Test]
    public void TryEvolveBlessing_SucceedsOnAlways()
    {
        var go = new GameObject("BlessingManager");
        var mgr = go.AddComponent<BlessingManager>();
        var def = CreateTestBlessingDefinition("bless_2", "Evolving Blessing", 2);
        mgr.RegisterDefinition(def);
        var inst = mgr.AcquireBlessing("bless_2");

        var evolved = mgr.TryEvolveBlessing(inst);
        Assert.IsTrue(evolved);
        Assert.AreEqual(1, inst.CurrentStageIndex);
        
        Object.DestroyImmediate(go);
    }
}
