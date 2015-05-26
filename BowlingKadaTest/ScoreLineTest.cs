using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BowlingKada;

namespace BowlingKadaTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ZeroGameTest()
        {
            BowlingScorer.reset();
            var result = BowlingScorer.ScoreLine("00000000000000000000");

            Assert.AreEqual(0, result);
        }


        [TestMethod]
        public void SinglePinTest()
        {
            BowlingScorer.reset();
            var result = BowlingScorer.ScoreLine("10000000000000000000");

            Assert.AreEqual(1, result);
        }
        [TestMethod]
        public void AllOnesPinTest()
        {
            BowlingScorer.reset();
            var result = BowlingScorer.ScoreLine("11111111111111111111");
            
            Assert.AreEqual(20, result);
        }

        [TestMethod]
        public void SingleSpareTest()
        {
            BowlingScorer.reset();
            var result = BowlingScorer.ScoreLine("1/000000000000000000");

            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void SingleStrikeTest()
        {
            BowlingScorer.reset();
            var result = BowlingScorer.ScoreLine("X0000000000000000000");

            Assert.AreEqual(10,result);
        }

        [TestMethod]
        public void SpareLookaheadNumericalTest()
        {
            BowlingScorer.reset();
            var result = BowlingScorer.ScoreLine("1/110000000000000000");

            Assert.AreEqual(13, result);
        }

        [TestMethod]
        public void StrikeLookaheadNumericalTest()
        {
            BowlingScorer.reset();
            var result = BowlingScorer.ScoreLine("X1110000000000000000");

            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void SpareLookaheadStrikeTest()
        {
            BowlingScorer.reset();
            var result = BowlingScorer.ScoreLine("1/X00000000000000000");

            Assert.AreEqual(30, result);
        }

        [TestMethod]
        public void StrikeLookaheadStrikeTest()
        {
            BowlingScorer.reset();
            var result = BowlingScorer.ScoreLine("XX000000000000000000");

            Assert.AreEqual(30, result);
        }

        [TestMethod]
        public void StrikeLookaheadSpareTest()
        {
            BowlingScorer.reset();
            var result = BowlingScorer.ScoreLine("X1/00000000000000000");

            Assert.AreEqual(30, result);
        }


        [TestMethod]
        public void FrameTenStrikeTest()
        {
            BowlingScorer.reset();
            var result = BowlingScorer.ScoreLine("000000000000000000X00");

            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void FrameTenStrikeLookaheadNumericalTest()
        {
            BowlingScorer.reset();
            var result = BowlingScorer.ScoreLine("0000000000000000000X11");

            Assert.AreEqual(12, result);
        }

        [TestMethod]
        public void FrameTenStrikeLookaheadSpareTest()
        {
            var result = BowlingScorer.ScoreLine("0000000000000000000X1/");

            Assert.AreEqual(20, result);
        }

//        [TestMethod]
//        public void PerfectGameTest()
//        {
//            var result = BowlingScorer.ScoreLine("XXXXXXXXXXXX");
//
//            Assert.AreEqual(300, result);
//        }
//
//        [TestMethod]
//        public void AlternatingFiveSpareTest()
//        {
//            var result = BowlingScorer.ScoreLine("5/5/5/5/5/5/5/5/5/5/5");
//
//            Assert.AreEqual(150, result);
//        }
//
//        [TestMethod]
//        public void AlternatingNineMissTest()
//        {
//            var result = BowlingScorer.ScoreLine("9-9-9-9-9-9-9-9-9-9-");
//
//            Assert.AreEqual(90, result);
//        }
        
    }
}
