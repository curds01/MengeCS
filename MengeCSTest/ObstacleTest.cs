using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MengeCS;

namespace MengeCSTest
{
    [TestClass]
    public class ObstacleTest
    {
        [TestMethod]
        public void TestWindingCCWComputation()
        {
            Obstacle o = new Obstacle();
            o.AddPoint(new Vector3(0, 0, 0));
            o.AddPoint(new Vector3(1, 0, 0));
            o.AddPoint(new Vector3(1, 0, 1));
            Assert.IsTrue(o.AgentsWalkOutside());
            o.ComputeWinding();
            Assert.IsTrue(o.AgentsWalkOutside());
        }

        [TestMethod]
        public void TestWindingCWComputation()
        {
            Obstacle o = new Obstacle();
            o.AddPoint(new Vector3(1, 0, 1));
            o.AddPoint(new Vector3(1, 0, 0));
            o.AddPoint(new Vector3(0, 0, 0));
            Assert.IsTrue(o.AgentsWalkOutside());
            o.ComputeWinding();
            Assert.IsFalse(o.AgentsWalkOutside());
        }
    }
}
