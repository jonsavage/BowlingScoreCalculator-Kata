using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BowlingKada;

namespace BowlingKadaTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SimpleNoSpareNoStrikeTest()
        {
            var result = BowlingScorer.ScoreLine("11111111111111111111");

            Assert.AreEqual(20, result);
        }

        [TestMethod]
        public void SingleMissTest()
        {
            var result = BowlingScorer.ScoreLine("1-111111111111111111");

            Assert.AreEqual(19, result);
        }

        [TestMethod]
        public void SingleSpareTest()
        {
            var result = BowlingScorer.ScoreLine("1/111111111111111111");

            Assert.AreEqual(29, result);
        }

        [TestMethod]
        public void SingleStrikeTest()
        {
            var result = BowlingScorer.ScoreLine("1X11111111111111111");

            Assert.AreEqual(30,result);
        }

        [TestMethod]
        public void DoubleStrikeTest()
        {
            var result = BowlingScorer.ScoreLine("XX1111111111111111");

            Assert.AreEqual(47, result);
        }

        [TestMethod]
        public void PerfectGameTest()
        {
            var result = BowlingScorer.ScoreLine("XXXXXXXXXXXX");

            Assert.AreEqual(300, result);
        }

        [TestMethod]
        public void AlternatingNineMissTest()
        {
            var result = BowlingScorer.ScoreLine("9-9-9-9-9-9-9-9-9-9-");

            Assert.AreEqual(90, result);
        }

        [TestMethod]
        public void AlternatingFiveSpareTest()
        {
            var result = BowlingScorer.ScoreLine("5/5/5/5/5/5/5/5/5/5/5");

            Assert.AreEqual(150, result);
        }
    }
}
