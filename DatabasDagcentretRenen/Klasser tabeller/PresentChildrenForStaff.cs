using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasDagcentretRenen.Klasser_tabeller
{
    public class PresentChildrenForStaff
    {
        public int Child_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Section { get; set; }
        public bool Breakfast { get; set; }

        public int NumberofBreakfast { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public string Action { get; set; }
        public int Serial_id { get; set; }

        public PresentChildrenForStaff()
        {
            Action = "'Närvarande'";
        }


    }
}
