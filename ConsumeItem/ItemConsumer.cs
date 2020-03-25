using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            //string ItemWebApi = "http://localhost:51407/api/localitems";

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
           // string EventWebApi = "http://localhost:51407/PostReturn";
            Item newItem = new Item(11,"mmmmm","Low", 22);

            using (HttpClient client = new HttpClient())
            {
                string newItemJson = JsonConvert.SerializeObject(newItem);
                var content = new StringContent(newItemJson, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(EventWebApi);
                client.DefaultRequestHeaders.Clear();
                
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsync("api/items", content);

                Console.WriteLine("********* An item posted to service ********");
                Console.WriteLine("********* Response is " + response + "********");
                response.EnsureSuccessStatusCode();
                var httpResponseBody = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(httpResponseBody);
                //if (response.IsSuccessStatusCode)
                //{

                //    // var responseEvent = client.GetAsync("api/localitems" + newItem).Result;

                //    //if (responseEvent.IsSuccessStatusCode)
                //    //{
                //    //     var Event = await responseEvent.Content.ReadAsStreamAsync<Item>().Result;
                //    //     string saveEvent =  responseEvent.Content.ReadAsStringAsync<Item>().Result;

                //    //}
                //}
            }

            Console.WriteLine("********* Get all items for verification ********");
            return await GetItemsHttpTask();
        }

    }
}
