using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ItemLibrary;
using Newtonsoft.Json;

namespace ConsumeItem2020
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World ItemConsumer ready to run !");
            Console.ReadKey();
            //string ItemWebApi = "https://itemsrvice.azurewebsites.net/api/items";
            // string ItemWebApi = "https://itemwebservicef20.azurewebsites.net/api/localitems";
            ItemConsumer getConsumer = new ItemConsumer();
            getConsumer.Start();
            //GetAndPrintItems(ItemWebApi);
            PostNewItem();
           // PostNewItem();
            PutNewItem();
            Console.ReadLine();
        }

        private static void GetAndPrintItems(string ItemWebApi)
        {
            Console.WriteLine("******************GET ALL items*************");
            List<Item> items = new List<Item>();
            try
            {
                Task<List<Item>> callTask = Task.Run(() => GetItems(ItemWebApi));
                callTask.Wait();
                items = callTask.Result;
                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine(items[i].ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static async Task<List<Item>> GetItems(string ItemWebApi)
        {
            using (HttpClient client = new HttpClient())
            {
                string eventsJsonString = await client.GetStringAsync(ItemWebApi);
                if (eventsJsonString != null)
                    return (List<Item>) JsonConvert.DeserializeObject(eventsJsonString, typeof(List<Item>));
                return null;
            }
        }

        private static void PostNewItem()
        {
            Console.WriteLine("******************POST a new Item*************");
            List<Item> items = new List<Item>();
            try
            {
                Task<List<Item>> callTask = Task.Run(() => PostItemHttpTask());
                callTask.Wait();
                items = callTask.Result;
                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine(items[i].ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public static async Task<List<Item>> PostItemHttpTask()
        {
            string ItemWebApiBase = "https://itemwebservicef20.azurewebsites.net/";
            // string EventWebApi = "http://localhost:51407/PostReturn";
            Item newItem = new Item(11, "mmmmm", "Low", 22);

            using (HttpClient client = new HttpClient())
            {
                string newItemJson = JsonConvert.SerializeObject(newItem);
                var content = new StringContent(newItemJson, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(ItemWebApiBase);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsync("api/localitems", content);

                Console.WriteLine("********* An item posted to service ********");
                Console.WriteLine("********* Response is " + response + "********");
                response.EnsureSuccessStatusCode();
                var httpResponseBody = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(httpResponseBody);
                
            }

            Console.WriteLine("********* Get all items for verification ********");
            string ItemWebApi = "https://itemwebservicef20.azurewebsites.net/api/localitems";
            return await GetItems(ItemWebApi);
        }

        private static void PutNewItem()
        {
            Console.WriteLine("******************Put a new Item*************");
            List<Item> items = new List<Item>();
            try
            {
                Task<List<Item>> callTask = Task.Run(() => PutItemHttpTask());
                callTask.Wait();
                items = callTask.Result;
                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine(items[i].ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static async Task<List<Item>> PutItemHttpTask()
        {
            string ItemWebApiBase = "https://itemwebservicef20.azurewebsites.net/";
            // string EventWebApi = "http://localhost:51407/PostReturn";
            Item newItem = new Item(11, "newEdited", "Low", 22);

            using (HttpClient client = new HttpClient())
            {
                string newItemJson = JsonConvert.SerializeObject(newItem);
                var content = new StringContent(newItemJson, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(ItemWebApiBase);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PutAsync("api/localitems/11", content);

                Console.WriteLine("********* An item posted to service ********");
                Console.WriteLine("********* Response is " + response + "********");
                response.EnsureSuccessStatusCode();
                var httpResponseBody = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(httpResponseBody);

            }

            Console.WriteLine("********* Get all items for verification ********");
            string ItemWebApi = "https://itemwebservicef20.azurewebsites.net/api/localitems";
            return await GetItems(ItemWebApi);
        }
    }
}
