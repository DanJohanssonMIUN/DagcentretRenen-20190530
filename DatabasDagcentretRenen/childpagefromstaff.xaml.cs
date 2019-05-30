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
    /// Interaction logic for childpagefromstaff.xaml
    /// </summary>
    public partial class childpagefromstaff : Window
    {
        MainWindow win1 = ((MainWindow)Application.Current.MainWindow);
        dbOpsRead dbread = new dbOpsRead();


        Klasser_tabeller.PresentChildrenForStaff activePc = new Klasser_tabeller.PresentChildrenForStaff();

        public childpagefromstaff()
        {
            InitializeComponent();
            activePc = win1.GetChild();
            lblChildName.Content = activePc.FirstName.ToString() + " " + activePc.LastName.ToString();
            lstKid.ItemsSource = null;
            lstKid.ItemsSource = dbread.GetChildInfo(activePc);

            lstGuardian.ItemsSource = null;
            lstGuardian.ItemsSource = dbread.GetGuardianInfo(activePc);

            lstPickup.ItemsSource = null;
            lstPickup.ItemsSource = dbread.GetGetterInfo(activePc);

            
            
        }
    }
}
