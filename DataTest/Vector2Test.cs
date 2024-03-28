using Data;

namespace DataTest
{
    [TestClass]
    public class Vector2Test
    {
        [TestMethod]
        public void AddTest()
        {
            var a = IVector2.Create(1, 2);
            var b = IVector2.Create(3, 4);
            var cx = a.X + b.X;
            var cy = a.Y + b.Y;
            Assert.AreEqual(4, cx);
            Assert.AreEqual(6, cy);
        }
        
        [TestMethod]
        public void SubtractTest()
        {
            var a = IVector2.Create(1, 2);
            var b = IVector2.Create(3, 4);
            var cx = a.X - b.X;
            var cy = a.Y - b.Y;
            Assert.AreEqual(-2, cx);
            Assert.AreEqual(-2, cy);
        }
        
        [TestMethod]
        public void MultiplyTest()
        {
            var a = IVector2.Create(1, 2);
            var ax = a.X * 3;
            var ay = a.Y * 3;
            Assert.AreEqual(3, ax);
            Assert.AreEqual(6, ay);
        }
        
        [TestMethod]
        public void DivideTest()
        {
            var a = IVector2.Create(1, 2);
            var ax = a.X / 2f;
            var ay = a.Y / 2f;
            Assert.AreEqual(0.5f, ax);
            Assert.AreEqual(1, ay);
        }
        
        [TestMethod]
        public void DistanceTest()
        {
            var a = IVector2.Create(1, 2);
            var b = IVector2.Create(4, 6);
            var distance = a.Distance(b);
            Assert.AreEqual(5, distance);
        }
    }
}