using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DatabasDagcentretRenen
{
    /// <summary>
    /// Interaction logic for CreateSchedule.xaml
    /// </summary>
    public partial class CreateSchedule : Window
    {
        VårdnadshavareMainWin winVh;
        dbOpsWrite dbw = new dbOpsWrite();
        dbOpsRead dbr = new dbOpsRead();

        Klasser_tabeller.Person activeP;
        Klasser_tabeller.GuardiansChild activeChild;

        List<Klasser_tabeller.Schedule> schedules;
        public CreateSchedule()
        {
            InitializeComponent();
            //activeP = winVh.GetPerson();
            //activeChild = winVh.GetChild();
        }

        public void FuckYou()
        {
            activeP = winVh.activeP;
            activeChild = winVh.activeChild;
        }

        public void FillList()
        {
            schedules = dbr.GetSchedule();
        }

        private void btnAddSchedule_Click(object sender, RoutedEventArgs e)
        {
            string date = calendar.ToString();
            string startTime = txtBoxStartTime.Text;
            string endTime = txtBoxEndTime.Text;
            bool breakfast = false;
            if (chkBreakfast.IsChecked == true)
            {
                breakfast = true;
            }

            dbw.AddSchedule(activeP.Person_ID, activeChild.Child_ID, date, startTime, endTime, breakfast);
            FillList();
            int last = schedules[schedules.Count - 1].Serial_nr;
            dbw.Presence(activeChild.Child_ID, activeChild.Action, last);
       
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
