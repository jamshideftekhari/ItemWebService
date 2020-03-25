using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ItemLibrary;
using Newtonsoft.Json;

namespace ConsumeItem2020
{
    class ItemConsumer
    {
        string ItemWebApi = "https://itemwebservicef20.azurewebsites.net/api/localitems";
        public async void Start()
        {
            Console.WriteLine("start method call");

            List<Item> itemlist = new List<Item>();
            itemlist = (List<Item>)await GetAllItemsAsync();


            foreach (var item in itemlist )
            {
                Console.WriteLine(item);
            }

        }

        //public async Task<List<Item>> GetItemsHttpTask()
        //{
        //    string ItemWebApi = "https://itemsrvice.azurewebsites.net/api/items";
        //    //string ItemWebApi = "http://localhost:51407/api/localitems";

        //    using (HttpClient client = new HttpClient())
        //    {
        //        string eventsJsonString = await client.GetStringAsync(ItemWebApi);
        //        if (eventsJsonString != null)
        //            return (List<Item>)JsonConvert.DeserializeObject(eventsJsonString, typeof(List<Item>));
        //        return null;
        //    }
        //}

        public async Task<IList<Item>> GetAllItemsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(ItemWebApi); 
                IList<Item> cList = JsonConvert.DeserializeObject<IList<Item>>(content); 
                return cList;
            }
        }
    }
}
