using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListManagement.models;

namespace ListManagement.models
{
    public class Task : Item
    {
        public bool IsCompleted { get; set; }

        public DateTime DeadLine { get; set; }

        public string Print()
        {
            return ( $"{Name} Completed: {IsCompleted}");
        }
        public override string ToString()
        {
            return $"{Name} {Description} Completed: {IsCompleted}";
        }

    }
}
