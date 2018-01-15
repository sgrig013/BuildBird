using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArchieBirds.Models;

namespace ArchieBirds.Tests
{
    [TestClass]
    public class ArchieTests
    {
        public static Archie bird;

        [ClassInitialize()]
        static public void Init(TestContext context)
        {
            bird = new Archie();
            bird.Feet = 2;
            bird.Girth = 10;
            bird.HandThings = 0;
            bird.Height = 50;
            bird.Length = 100;
            bird.Latitude = 1.11;
            bird.Longtitude = 2.22;
            bird.Skull = 1;
            bird.SpecimenName = "Archie";
            bird.Spine = 1;
            bird.Tail = 1;
            bird.Teeth = 123;
            bird.Weight = 100;
            bird.Wings = 2;
            bird.WingWidth = 30;
        }

        [TestMethod]
        public void TestWingspanCalculation()
        {
            double wingspan = Decimal.ToDouble(bird.WingWidth * 2 + bird.Girth);
            Assert.AreEqual(wingspan, bird.Wingspan);

            bird.Girth = 0;
            wingspan = Decimal.ToDouble((bird.WingWidth * 2) + (bird.Length / 6));
            Assert.AreEqual(wingspan, bird.Wingspan);

            bird.Wings = 0;
            Assert.AreEqual(0, bird.Wingspan);
        }

        [TestMethod]
        public void TestToMetricCalculation()
        {
            bird.currentUnit = Units.Imperial;
            bird.Height = 10m;
            bird.ToMetric();
            Assert.AreEqual(25.4m, bird.Height);
            Assert.AreEqual(Units.Metric, bird.currentUnit);
        }

        [TestMethod]
        public void TestSetUnitMetricToMilliops()
        {
            bird.currentUnit = Units.Metric;
            bird.Height = 177.8m;
            // mops is 1/7th of an inch
            var expectedHeight = bird.Height / 2.54m / 7.0m;
            bird.SetUnit(Units.Milliarchieops);
            Assert.AreEqual(expectedHeight, bird.Height);
        }

        [TestMethod]
        public void TestSetUnitMetricToImperial()
        {
            bird.currentUnit = Units.Metric;
            bird.Height = 254m;
            bird.SetUnit(Units.Imperial);
            Assert.AreEqual(100m, bird.Height);
        }

        [TestMethod]
        public void TestSetUnitImperialToMilliops()
        {
            bird.currentUnit = Units.Imperial;
            bird.Height = 700m;
            // mops is 1/7th of an inch
            var expectedHeight = bird.Height / 7.0m;
            bird.SetUnit(Units.Milliarchieops);
            Assert.AreEqual(expectedHeight, bird.Height);
        }
    }
}
