using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasDagcentretRenen.Klasser_tabeller
{
    class Presence
    {
        public int Serial_nr { get; set; }
        public DateTime Real_presence { get; set; }
        public bool Went_home { get; set; }

        public int Child_ID { get; set; }
        public int Staff_ID { get; set; }
        
    }
}
