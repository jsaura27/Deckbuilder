using System;
using UnityEditor;
using UnityEngine;

public static class ScriptableObjectDiagnostics
{
    [MenuItem("Tools/Diagnostics/ScriptableObject Create Test")]
    public static void RunCreateTests()
    {
        try
        {
            Debug.Log("--- ScriptableObject Diagnostics ---");

            TestType("Deckbuilder.DataModels.EffectDefinition");
            TestType("Deckbuilder.DataModels.CardDefinition");

            Debug.Log("--- Diagnostics complete ---");
        }
        catch (Exception ex)
        {
            Debug.LogError("Diagnostics threw: " + ex);
        }
    }

    private static void TestType(string typeName)
    {
        Debug.Log($"Testing type: {typeName}");
        var asmQualified = typeName + ", Assembly-CSharp";
        try
        {
            var t = Type.GetType(asmQualified);
            Debug.Log("Type.GetType(asmQualified) -> " + (t != null) + " (" + asmQualified + ")");
            if (t != null)
            {
                Debug.Log($"IsClass={t.IsClass} IsAbstract={t.IsAbstract} IsSubclassOf<ScriptableObject>={t.IsSubclassOf(typeof(ScriptableObject))}");
            }

            // Try ScriptableObject.CreateInstance
            try
            {
                var so = ScriptableObject.CreateInstance(t);
                Debug.Log("ScriptableObject.CreateInstance returned: " + (so != null) + (so != null ? " type=" + so.GetType().FullName : ""));
            }
            catch (Exception ex)
            {
                Debug.LogError("ScriptableObject.CreateInstance threw: " + ex.Message);
            }

            // Try Activator
            try
            {
                var obj = Activator.CreateInstance(t);
                Debug.Log("Activator.CreateInstance returned: " + (obj != null) + (obj != null ? " type=" + obj.GetType().FullName : ""));
            }
            catch (Exception ex)
            {
                Debug.LogError("Activator.CreateInstance threw: " + ex.Message);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error testing type " + typeName + ": " + ex.Message);
        }
    }
}
