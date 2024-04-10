using ClientData;

namespace ClientDataTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void CreateTest()
        {
            IData data = IData.Create(null);
            Assert.IsNotNull(data);
        }
    }
}