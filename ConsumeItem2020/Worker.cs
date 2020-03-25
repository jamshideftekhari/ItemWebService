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
    // this class is not used
    class Worker
    {
        private static string URI = "https://itemsrvice.azurewebsites.net/api/Items";
        string URI2 = "https://itemsrvice.azurewebsites.net/api/Items/0";
        public async void Start()
        {
            List<Item> Itemliste = new List<Item>();
            Itemliste = (List<Item>)await GetAllItemsAsync();
            foreach (var item in Itemliste)
            {
                Console.WriteLine(item.ToString());
            }
            Item Itemliste2 = new Item();

            Itemliste2 = await GetOneItemAsync(0);
            Console.WriteLine("++ Get One Items ++");

            Console.WriteLine(Itemliste2);
            Console.WriteLine("++ Get One Items ++");

        }

        public static async Task<IList<Item>> GetAllItemsAsync()
        {
            Console.WriteLine("++ Get All Items ++");
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(URI);
                IList<Item> cList = JsonConvert.DeserializeObject<IList<Item>>(content);
                return cList;
            }
        }

        public async Task<Item> GetOneItemAsync(int id)
        {
            Console.WriteLine("++ Get One Items ++");

            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(URI);
                if (content != null)
                {
                    IList<Item> cItem =
                        JsonConvert.DeserializeObject<IList<Item>>(content);
                    return cItem[id];
                }
                else
                {
                    return null;
                }
            }
        }

        public static void PostNewItem()
        {
           
            try
            {
                Console.WriteLine("++ Post New Item++");
                List<Item> items = new List<Item>();
                Task<List<Item>> callTask = Task.Run(() => PostItemHttpTask());
                    {
                    callTask.Wait();
                    items = callTask.Result;
                    for (int i = 0; i < items.Count; i++)
                    {
                        Console.WriteLine(items[i].ToString());
                    }
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
            string URI = "https://itemsrvice.azurewebsites.net/api/Items";
            Item newItem = new Item();

            using (HttpClient client = new HttpClient())
            {
                string newItemJson = JsonConvert.SerializeObject(newItem);
                var content = new StringContent(newItemJson, Encoding.UTF8, "application/Json");
                client.BaseAddress = new Uri(URI);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/Json"));
                HttpResponseMessage response = await client.PostAsync("application/Json", content);
                Console.WriteLine("********* An item posted to service ********");
                Console.WriteLine("********* Response is " + response + "********");

                var httpResponseBody = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(httpResponseBody);
            }

            Console.WriteLine("++ Get All Items For Verification ++");
            string ItemWebAPI = "https://itemsrvice.azurewebsites.net/api/Items";
            return (List<Item>)await GetAllItemsAsync();
        }
    }
}
