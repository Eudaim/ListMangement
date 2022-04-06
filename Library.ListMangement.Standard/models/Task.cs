using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListManagement.interfaces;

namespace ListManagement.models
{
    public class Task : Item
    {
        public bool showCheckBox = true;
        public bool IsCompleted { get; set; } = false;

        public DateTimeOffset DeadLine { get; set; } = System.DateTimeOffset.MinValue; 

        public string Deadline
        {
            get
            {
                string starttime = DeadLine.DateTime.ToString();
                return starttime;
            }

            set
            {
                string starttime = DeadLine.DateTime.ToString();

            }
        }

        public string Print()
        {
            return ( $"{Name} Completed: {IsCompleted}");
        }
        public override string ToString()
        {
            return $"{Name} {Description} Completed: {IsCompleted} Priority: {Priority}";
        }

    }
}
