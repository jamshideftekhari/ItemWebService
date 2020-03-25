using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemLibrary;
using ItemWebService.Persistency;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItemWebService.Controllers
{
    [Route("api/[controller]")]
   // [Route("api/LocalItems")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
       private static readonly List<Item> items = new List<Item>()
            { new Item(1, "Bread", "Low", 33),
              new Item(2, "Bread", "Middle", 21),
              new Item(3, "Beer", "low", 70.5),
              new Item(4, "Soda", "High", 21.4),
              new Item(5, "Milk", "Low", 55.8)
            };
        // GET: api/Items
        [HttpGet]
        public IEnumerable<Item> Get()
        {
            PersistencyService persistency = new PersistencyService();
           // return items;
           return persistency.Get();
        }

        // GET: api/Items/5
        [HttpGet("{id}", Name = "Get")]
        //[HttpGet]
        //[Route("{id}")]
        public Item Get(int id)
        {
            // return items.Find(i => i.Id == id);
            PersistencyService persistance = new PersistencyService();
            return persistance.Get(id);
            
        }

        //// POST: api/Items
        //[HttpPost]
        //public void Post([FromBody] Item value)
        //{
        //    items.Add(value);
        //    // how to make this method better? return the added item. 
        //}

        // POST: api/Items
        [HttpPost]
        public Item Post([FromBody] Item value)
        {
           // items.Add(value);
            // how to make this method better? return the added item.
           // return Get(value.Id);
           PersistencyService persistance = new PersistencyService();
           persistance.PostItems(value);
           return Get(value.Id);
        }

        // PUT: api/Items/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Item value)
        {
            //Item item = Get(id);
            //if (item != null)
            //{ item.Id = value.Id;
            //  item.Name = value.Name;
            //  item.Quality = value.Quality;
            //  item.Quantity = value.Quantity; }
            PersistencyService persistance = new PersistencyService();
            persistance.PutItems(id, value);

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Item item = Get(id);
            items.Remove(item);
        }
        [HttpGet]
        [Route("Name/{substring}")]
        public IEnumerable<Item> GetFromSubstring(string substring)
        {
            return items.FindAll(i => i.Name.Contains(substring));
        }

        [HttpGet]
        [Route("Quality/{substring}")]
        public IEnumerable<Item> GetFromQSubstring(string substring)
        {
            return items.FindAll(i => i.Name.Contains(substring));
        }

        [HttpGet]
        [Route("GetWithQuery/")]
        public ActionResult<IEnumerable<string>> GetWithQuery([FromQuery]int request)
        {
            return new string[] { "value" + request, "value2" };
        }

        [HttpGet]
        [Route("GetItemIndex/")]
        public Item GetItem([FromQuery]int Index)
        {
            return items[Index];
        }

        [HttpGet]
        [Route("Search/LowQuantity")]
        public Item SearchLow([FromQuery] double number)
        {
            return items.Find(i => i.Quantity < number);
        }


        //search with object parameter
        [HttpGet]
        [Route("Search/")]
        public Item Search([FromQuery] FilterItem filter)
        {
            return items.Find(i => i.Quantity < filter.LowQuantity);
        }
    }
}
