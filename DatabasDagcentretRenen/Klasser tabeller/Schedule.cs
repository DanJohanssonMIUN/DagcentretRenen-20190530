using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasDagcentretRenen.Klasser_tabeller
{
    class Schedule
    {
        public int Serial_nr { get; set; }
        public bool Presence_absence { get; set; }
        public string Type_of_absence { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime Start_time { get; set; }
        public DateTime End_time { get; set; }
        public bool Breakfast { get; set; }

        public int Child_id { get; set; }
        public int Person_id { get; set; }
    }
}
