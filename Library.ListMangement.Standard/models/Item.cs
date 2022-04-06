using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using ListManagement.interfaces;


namespace ListManagement.models
{
   
    public class Item : IItem
        {
            private string _name;
            public string Name
            {
                get
                {
                    return _name;
                }
                set
                {
                    _name = value;
                }
            }
            public string Description { get; set; }

            public int ID { get; set; }
            public bool Priority { get; set; } = false;
            public override string ToString()
            {
                return $"{Name} {Description}";
            }

        }
  
   
}
