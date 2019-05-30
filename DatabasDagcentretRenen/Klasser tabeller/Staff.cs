using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasDagcentretRenen.Klasser_tabeller
{
    public class Staff
    {
        public int Staff_id { get; set; }
        public string PhoneNr { get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Room_ID { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
