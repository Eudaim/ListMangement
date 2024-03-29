﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ListManagement.helpers;
using ListManagement.models;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;


namespace ListManagement.services
{
    public class ItemService
    {
        private ObservableCollection<Item> items;
        // private List<Item> items;
        private ListNavigator<Item> listNav;
        private string userData;
        private int size = 1;
        private JsonSerializerSettings serializerSettings
           = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        private string persistencePath;

        static private ItemService instance;
        public ObservableCollection<Item> Items
        {
            get
            {
                return items;
            }
        }
        public bool ShowComplete { get; set; }
        public string Query { get; set; }
        public IEnumerable<Item> FilteredItems
        {
            get
            {
                var incompleteItems = Items.Where(i =>
                (!ShowComplete && !((i as Task)?.IsCompleted ?? true)) //incomplete only
                || ShowComplete);
                //show complete (all)

                var searchResults = incompleteItems.Where(i => string.IsNullOrWhiteSpace(Query)
                //there is no query
                || (i?.Name?.ToUpper()?.Contains(Query.ToUpper()) ?? false)
                //i is any item and its name contains the query
                || (i?.Description?.ToUpper()?.Contains(Query.ToUpper()) ?? false)
                //or i is any item and its description contains the query
                || ((i as Appointment)?.Attendees?.Select(t => t.ToUpper())?.Contains(Query.ToUpper()) ?? false));
                //or i is an appointment and has the query in the attendees list
                return searchResults;
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
            items = new ObservableCollection<Item>();

            persistencePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\SaveData.json";


            listNav = new ListNavigator<Item>(FilteredItems, 2);
        }

        public void Add(Item i)
        {
            if (i.ID <= 0)
            {
                i.ID = nextID;
            }
            items.Add(i);

        }

        public void Remove(Item i)
        {
            items.Remove(i);
        }

        public void Save()
        {

            var listJson = JsonConvert.SerializeObject(Items, serializerSettings);
            if (File.Exists(persistencePath))
            {
                File.Delete(persistencePath);
            }
            File.WriteAllText(persistencePath, listJson);
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
        private int nextID
        {
            get
            {
                if (Items.Any())
                {
                    return Items.Select(i => i.ID).Max() + 1;
                }
                return 1;
            }
        }
    }
}
