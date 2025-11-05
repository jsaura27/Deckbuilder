using NUnit.Framework;
using UnityEngine;

namespace Tests.Integration.RunState
{
    public class RunStateTests
    {
        [Test]
        public void RunState_Poco_Instantiate_Defaults()
        {
            // Qualify the RunState type with global:: to avoid collision with this test namespace (RunState)
            var rs = new global::RunState();
            Assert.IsNotNull(rs.runId);
            Assert.Greater(rs.seed, 0);
            Assert.IsNotNull(rs.startedAt);
            Assert.AreEqual(1, rs.playerProgress.level);
        }

        [Test]
        public void RunStateManager_CreateNewRun_SetsCurrent()
        {
            var go = new GameObject("RunStateManagerTest");
            var mgr = go.AddComponent<RunStateManager>();
            var run = mgr.CreateNewRun(12345);
            Assert.IsNotNull(mgr.CurrentRun);
            Assert.AreEqual(12345, mgr.CurrentRun.seed);
            GameObject.DestroyImmediate(go);
        }
    }
}