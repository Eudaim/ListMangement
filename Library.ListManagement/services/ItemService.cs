using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListManagement.helpers;
using ListManagement.models;
using Newtonsoft.Json;
using System.IO;

namespace ListManagement.services
{
    public class ItemService
    {
        private List<Item> items;
        private ListNavigator<Item> listNav;
        private string userData;

        static private ItemService instance;
        public List<Item> Items
        {
            get
            {
                return items;
            }
        }

        public IEnumerable<Item> IncompleteItems
        {
            get
            {
                return Items.Where(i => !((i as Task)?.IsCompleted ?? true));
            }
        }

        public static ItemService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new ItemService();
                }
                return instance;
            }
        }

        private ItemService()
        {
            items = new List<Item>();
            listNav = new ListNavigator<Item>(items, 5);
        }

        public void Add(Item i)
        {
            items.Add(i);
        }

        public void Remove(Item i)
        {
            items.Remove(i);
        }

        public void Save()
        {
            var persistencePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            userData = JsonConvert.SerializeObject(Items);
            if (File.Exists(persistencePath + "/userdata.json"))
            {
                File.Delete(persistencePath + "/userdata.json");
            }
            using (FileStream fs = File.Create(persistencePath + "/userdata.json"))
            {
               fs.Write(UnicodeEncoding.UTF8.GetBytes(userData));
            }

        }

        public void Load()
        {
            var persistencePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            if (File.Exists(persistencePath + "/userdata.json"))
            {
                var userSavedData = File.ReadAllText(persistencePath + "/userdata.json"); 
                var savedData = JsonConvert.DeserializeObject<List<Item>>(userSavedData);
                foreach (var ito in savedData)
                { 
                    Console.WriteLine(ito);
                }
            }
            else
            {
                Console.WriteLine("No previous data detected");
            }
        }

        public Dictionary<object, Item> GetPage()
        {
            var page = listNav.GetCurrentPage();
            if (listNav.HasNextPage)
            {
                page.Add("N", new Item { Name = "Next" });
            }
            if (listNav.HasPreviousPage)
            {
                page.Add("P", new Item { Name = "Previous" });
            }
            return page;
        }

        public Dictionary<object, Item> NextPage()
        {
            return listNav.GoForward();
        }

        public Dictionary<object, Item> PreviousPage()
        {
            return listNav.GoBackward();
        }
    }
}
