using NUnit.Framework;

public class MetaProgressionDataTests
{
    [Test]
    public void CanInstantiateMetaProgressionData()
    {
        var data = new MetaProgressionData();
        Assert.IsNotNull(data);
        Assert.IsNotNull(data.unlockedClasses);
    }
}
