using NUnit.Framework;
using UnityEngine;

namespace Tests.Integration.Services
{
    public class MetaProgressionServiceTests
    {
        [Test]
        public void SaveLoadCycle_PreservesVersion()
        {
            var svc = new MetaProgressionService();
            svc.SetData(new MetaProgressionData { version = "1.0-test" });
            svc.Save();
            svc.Load();
            Assert.AreEqual("1.0-test", svc.Data.version);
        }
    }
}
