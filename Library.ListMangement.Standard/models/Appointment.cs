using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListManagement.models;
using System.Globalization;

namespace ListManagement.models
{
        public class Appointment : Item
        {
            
            public bool showCheckBox =  false;
            public DateTimeOffset Start { get; set; } = System.DateTimeOffset.MinValue;
            public DateTimeOffset End { get; set; } = System.DateTimeOffset.MinValue;

            public List<string> Attendees { get; set; }
            
             
            public Appointment()
            {
                Attendees = new List<string>();
            }

            public override string ToString()
            {
                String startDateTime = Start.DateTime.ToString();
                String endDateTime = End.DateTime.ToString();
                return $"{Name} {Description} From {startDateTime} to {endDateTime}";
              
            }
            public string starttime
            {
                get
                {
                   string starttime = Start.DateTime.ToString();
                   return starttime;
                }

                set
                {
                    string starttime = Start.DateTime.ToString();
                  
                }
            }
            
            public string endtime
            {
                get 
                {
                    String endtime = End.DateTime.ToString();
                    return endtime;
                }
                set
                {
                    string starttime = Start.DateTime.ToString();
                }
            }

            public string attendees
            {
                get
                {
                    string attendees = string.Join(",", Attendees);
                    return attendees;
                }
                set
                {
                    string attendees = string.Join(",", Attendees);
                }
            }
        }

}
