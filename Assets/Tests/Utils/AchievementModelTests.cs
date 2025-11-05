using NUnit.Framework;

public class AchievementModelTests
{
    [Test]
    public void Instantiate_AchievementModel_DefaultsAccessible()
    {
        var m = new AchievementModel();
        Assert.IsNotNull(m);
        Assert.IsNull(m.id);
    }
}
