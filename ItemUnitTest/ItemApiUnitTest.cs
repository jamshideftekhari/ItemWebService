using System.Linq;
using ItemLibrary;
using ItemWebService.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ItemUnitTest
{
    [TestClass]
    public class ItemApiUnitTest
    {
        private Item _testItem;
        private ItemsController _testItemsController;

        [TestInitialize]
        public void InitTestItems()
        {
            _testItem = new Item(1, "Bread", "Low", 33);
            _testItemsController = new ItemsController();
        }


        [TestMethod]
        public void TestGetAll()
        {
            int itemsCount = _testItemsController.Get().Count();
            Assert.AreEqual(5,itemsCount);
        }

        [TestMethod]
        public void TestGetAll2()
        {
            int itemsCount = _testItemsController.Get().Count();
            Assert.AreEqual(5, itemsCount);
        }
    }
}
