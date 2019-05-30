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
    /// Interaction logic for MsgBoxAbsence.xaml
    /// </summary>
    public partial class MsgBoxAbsence : Window
    {
        MainWindow win1 = ((MainWindow)Application.Current.MainWindow);
        dbOpsWrite dbWrite = new dbOpsWrite();
        Klasser_tabeller.PresentChildrenForStaff activePc = new Klasser_tabeller.PresentChildrenForStaff();
        Klasser_tabeller.Staff activeStaff = new Klasser_tabeller.Staff();

        public MsgBoxAbsence()
        {
            InitializeComponent();
            activePc = win1.GetChild();
            activeStaff = win1.GetStaff();
            lblChildName.Content = activePc.FirstName.ToString() + " " + activePc.LastName.ToString();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnPickedup_Click(object sender, RoutedEventArgs e)
        {
            string date = win1.dateP.ToString();
            string type = "'Upphämtad'";
            dbWrite.AddPresence(activePc.Serial_id, activePc.Child_ID, type, date);

            Close();
        }

        private void btnAbsent_Click(object sender, RoutedEventArgs e)
        {
            string date = win1.dateP.ToString();
            string type = "'Frånvarande'";
            dbWrite.AddPresence(activePc.Serial_id, activePc.Child_ID, type, date);
            Close();
        }
    }
}
