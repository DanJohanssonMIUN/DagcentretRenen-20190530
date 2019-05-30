using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasDagcentretRenen.Klasser_tabeller
{
    public class GuardiansChild
    {
        public int Child_ID { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Special_needs { get; set; }
        public string Allergy { get; set; }
        public bool Walk_alone { get; set; }

        public string Action { get; set; }

        public string TypeOfAbsence { get; set; }

        public override string ToString()
        {
            return First_name + " " + Last_name;
        }

    }
}
