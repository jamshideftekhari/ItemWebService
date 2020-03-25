using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemLibrary;
using ItemWebServiceF20.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItemWebServiceF20.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/LocalItems")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
    //    private static readonly List<Item> items = new List<Item>()
    //    {
    //        new Item(1, "Bread", "Low", 33),
    //        new Item(2, "Bread", "Middle", 21),
    //        new Item(3, "Beer", "low", 70.5),
    //        new Item(4, "Soda", "High", 21.4),
    //        new Item(5, "Milk", "Low", 55.8)
    //    };

    private static readonly ItemContext _ctx = new ItemContext();
    public List<Item> items;

    /// <summary>
    /// adding a test list in the constructor
    /// </summary>
        public ItemsController()
        {
            items = _ctx.Items;
        }

    /// <summary>
    /// API/LocatItems
    /// return all items in the methode.  
    /// </summary>
    /// <returns></returns>

        // GET: api/Items
        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return items;
        }

        // GET: api/Items/5
        // [HttpGet("{id}", Name = "Get")]
        [HttpGet]
        [Route("{id}")]
        public Item Get(int id)
        {
            return items.Find(i=>i.Id == id);
        }

        [HttpGet]
        [Route("GetNew/{id}")]
        public ActionResult<Item> GetNew(int id)
        {
            var item = items.Find(i => i.Id == id);
            if (item == null)
            {
                return NotFound("Item Not Found");

            }
            return item;
        }

        // POST: api/Items
        [HttpPost]
        public void Post([FromBody] Item value)
        {
            items.Add(value);
        }

        // POST: api/Items
        [HttpPost]
        [Route("PostReturn")]
        public ActionResult<Item> PostReturn([FromBody] Item value)
        {
            items.Add(value);
            return CreatedAtAction("GetNew", new{id=value.Id}, value);
        }

        // PUT: api/Items/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Item value)
        {
            Item item = Get(id);
            if (item != null)
            {
                item.Id = value.Id;
                item.Name = value.Name;
                item.Quality = value.Quality;
                item.Quantity = value.Quantity;
            }
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
            return new string[] { "Request= " + request, "Returned by API Methode" };
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
