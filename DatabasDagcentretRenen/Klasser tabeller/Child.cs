using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasDagcentretRenen.Klasser_tabeller
{
    class Child
    {
        public int Child_ID { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Special_needs { get; set; }
        public string Allergy { get; set; }
        public bool Walk_alone { get; set; }

        public int Room_ID { get; set; }

        public List<PresentChildrenForStaff> kids { get; set; }
        public Child()
        {
            kids = new List<PresentChildrenForStaff>();
        }
        
        public override string ToString()
        {
            dbOpsWrite dbw = new dbOpsWrite();
            string mess = "";

            if (Walk_alone == true)
            {
                mess +="Får gå hem själv: Ja"; 
            }
            else if (Walk_alone == false)
            {
                mess +="Får gå hem själv: Nej";
            }
            if (!String.IsNullOrEmpty(Allergy))
            {
                mess += "\nAllergi " + Allergy;
            }
            if (!String.IsNullOrEmpty(Special_needs))
            {
                mess += "\nFunktionsvariation: " + Special_needs;
            }

            return mess;
        }
    }
}
