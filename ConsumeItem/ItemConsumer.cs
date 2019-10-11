using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ItemLibrary;
using Newtonsoft.Json;

namespace ConsumeItem
{
    public class ItemConsumer
    {
        public void Start()
        {
            Console.WriteLine("start method call");
        }

        public async Task<List<Item>> GetItemsHttpTask()
        {
            string ItemWebApi = "https://itemsrvice.azurewebsites.net/api/items";

            using (HttpClient client = new HttpClient())
            {
                string eventsJsonString = await client.GetStringAsync(ItemWebApi);
                if (eventsJsonString != null)
                    return (List<Item>)JsonConvert.DeserializeObject(eventsJsonString, typeof(List<Item>));
                return null;
            }
        }

        public async Task<List<Item>> PostItemHttpTask()
        {
            string EventWebApi = "https://itemsrvice.azurewebsites.net/";
            Item newItem = new Item(11,"mmmmm","Low", 22);

            using (HttpClient client = new HttpClient())
            {
                string newItemJson = JsonConvert.SerializeObject(newItem);
                var content = new StringContent(newItemJson, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(EventWebApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsync("api/items", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseEvent = client.GetAsync("api/items" + newItem).Result;
                    if (responseEvent.IsSuccessStatusCode)
                    {
                        // var Event = responseEvent.Content.ReadAsStreamAsync<Event>().Result;
                        //   string saveEvent = await responseEvent.Content.ReadAsStringAsync<Event>().Result;

                    }
                }
            }

            return await GetItemsHttpTask();
        }

    }
}
