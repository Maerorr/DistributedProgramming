namespace DataTest
{
    [TestClass]
    public class AddClassTest
    {
        [TestMethod]
        public void AddTest()
        {
            int ab = Data.AddClass.Add(1, 2);
            Assert.AreEqual(3, ab);
        }
    }
}