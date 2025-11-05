using System.IO;
using NUnit.Framework;

public class SavePlanPresenceTests
{
    private const string PlanPath = "Assets/docs/plans/save-system-migration/plan.json";

    [Test]
    public void PlanFile_Exists()
    {
        Assert.IsTrue(File.Exists(PlanPath), $"Expected plan file at {PlanPath}");
    }

    [Test]
    public void PlanFile_ContainsTaskName()
    {
        var text = File.ReadAllText(PlanPath);
        Assert.IsTrue(text.Contains("\"task\": \"Save System & Migration\""), "plan.json should contain the task name");
    }
}
