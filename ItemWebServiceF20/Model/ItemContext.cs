using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemLibrary;

namespace ItemWebServiceF20.Model
{
    public class ItemContext
    {
        public List<Item> Items { get; set; }

        public ItemContext()
        {
            Items = new List<Item>();
            if (Items.Count == 0)
            {
                Items.Add(new Item{Id = 0, Name = "Item 0", Quality = "Not applicable", Quantity = -1});
            }
        }
    }
}
