using System.Linq;
using ItemLibrary;
using ItemWebServiceF20.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ItemUnitTestF20
{
    [TestClass]
    public class UnitTest1
    {
        private Item _testItem1, _testItem2, _testItem3, _testItem4;
        private ItemsController _testItemsController;

        [TestInitialize]
        public void InitTestItems()
        {
            _testItem1 = new Item(1, "Bread", "Low", 33);
            _testItem2 = new Item(2, "Beer", "High", 20);
            _testItem3 = new Item(3, "Milk", "High", 10);
            _testItem4 = new Item(4, "CoCa", "Low", 15);
            _testItemsController = new ItemsController();
        }


        [TestMethod]
        public void TestGetAll()
        {
            int itemsCount = _testItemsController.Get().Count();
            Assert.AreEqual(1, itemsCount);
        }

        [TestMethod]
        public void TestGetAll_Post()
        {
            _testItemsController.Post(_testItem1);
            int itemsCount = _testItemsController.Get().Count();
            Assert.AreEqual(2, itemsCount);
        }

        [TestMethod]
        public void TestGetById()
        {
            _testItemsController.Post(_testItem2);
            Item item = _testItemsController.Get(1);
            Assert.AreEqual(3, _testItemsController.Get().Count());
            Assert.AreEqual(33, item.Quantity);
            
        }

        [TestMethod]
        public void TestGetById1()
        {
            _testItemsController.Post(_testItem3);
            Item item = _testItemsController.Get(1);
            Assert.AreEqual(4, _testItemsController.Get().Count());
            Assert.AreEqual("Bread", item.Name);
        }

        [TestMethod]
        public void TestGet_put()
        {
            _testItemsController.Put(2,_testItem4);
            Item item = _testItemsController.Get(4);
            Assert.AreEqual(4, _testItemsController.Get().Count());
            Assert.AreEqual("CoCa", item.Name);
            Assert.AreEqual("Low", item.Quality);
            Assert.AreEqual(15, item.Quantity);
        }
    }
}
