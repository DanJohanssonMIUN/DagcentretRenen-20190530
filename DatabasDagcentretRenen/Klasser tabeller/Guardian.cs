using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasDagcentretRenen.Klasser_tabeller
{
    class Guardian
    {
        public int Person_ID { get; set; }
        public int Child_ID { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Phone_nr { get; set; }

        public override string ToString()
        {
            return First_name + " " + Last_name + "\nTelnr: " + Phone_nr;
        }

    }
    
    

}
