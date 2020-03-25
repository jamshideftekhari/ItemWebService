using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ItemLibrary;

namespace ConsumeItem
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("main program start!");
            ItemConsumer getConsumer = new ItemConsumer();
            getConsumer.Start();
            //
            GetAllItems(getConsumer);
            PostNewItem(getConsumer);
           
            
            Console.ReadLine();
        }

        private static void PostNewItem(ItemConsumer getConsumer)
        {
            try
            {
                Console.WriteLine("******************POST a new Item*************");
                List<Item> items = new List<Item>();
                Task<List<Item>> callTask = Task.Run(() => getConsumer.PostItemHttpTask());
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

        private static void GetAllItems(ItemConsumer getConsumer)
        {
            try
            {
                Console.WriteLine("******************GET ALL items*************");
                List<Item> items = new List<Item>();
                Task<List<Item>> callTask = Task.Run(() => getConsumer.GetItemsHttpTask());
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
    }
}
