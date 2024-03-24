using Data;

namespace DataTest
{
    [TestClass]
    public class Vector2Test
    {
        [TestMethod]
        public void AddTest()
        {
            var a = new Vector2(1, 2);
            var b = new Vector2(3, 4);
            var c = a + b;
            Assert.AreEqual(4, c.X);
            Assert.AreEqual(6, c.Y);
        }
        
        [TestMethod]
        public void SubtractTest()
        {
            var a = new Vector2(1, 2);
            var b = new Vector2(3, 4);
            var c = a - b;
            Assert.AreEqual(-2, c.X);
            Assert.AreEqual(-2, c.Y);
        }
        
        [TestMethod]
        public void MultiplyTest()
        {
            var a = new Vector2(1, 2);
            var b = a * 3;
            Assert.AreEqual(3, b.X);
            Assert.AreEqual(6, b.Y);
        }
        
        [TestMethod]
        public void DivideTest()
        {
            var a = new Vector2(1, 2);
            var b = a / 2;
            Assert.AreEqual(0.5f, b.X);
            Assert.AreEqual(1, b.Y);
        }
        
        [TestMethod]
        public void DistanceTest()
        {
            var a = new Vector2(1, 2);
            var b = new Vector2(4, 6);
            var distance = a.Distance(b);
            Assert.AreEqual(5, distance);
        }
    }
}