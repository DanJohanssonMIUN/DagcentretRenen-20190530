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
using Npgsql;
using System.Configuration;

namespace DatabasDagcentretRenen
{
    /// <summary>
    /// Interaction logic for VårdnadshavareMainWin.xaml
    /// </summary>
    public partial class VårdnadshavareMainWin : Window
    {
        CreateSchedule winCs = new CreateSchedule();
        dbOpsRead dbr = new dbOpsRead();
        dbOpsWrite dbw = new dbOpsWrite();

        public Klasser_tabeller.Person activeP;
        public Klasser_tabeller.GuardiansChild activeChild;
        public VårdnadshavareMainWin()
        {
            InitializeComponent();
            FillCmbGuardian();
        }
        public Klasser_tabeller.Person GetPerson()
        {
            return activeP;
        }
        public Klasser_tabeller.GuardiansChild GetChild()
        {
            return activeChild;
        }


        public void FillCmbGuardian()
        {
            cmbGuardian.ItemsSource = null;
            cmbGuardian.ItemsSource = dbr.GetPersons();
        }

        public void FillCmbChild()
        {

                cmbChild.ItemsSource = null;
                cmbChild.ItemsSource = dbr.GetChildren(activeP.Person_ID);
            

        }

        private void cmbGuardian_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            activeP = (Klasser_tabeller.Person)cmbGuardian.SelectedItem;
            FillCmbChild();
        }



        private void btnChangeGame_Click(object sender, RoutedEventArgs e)
        {


            MainWindow win1 = new MainWindow();
            win1.Show();
            this.Hide();
        }

        private void CmbChild_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

           activeChild = (Klasser_tabeller.GuardiansChild)cmbChild.SelectedItem;
            if (activeChild != null)
            {
                lsvChildSchedule.ItemsSource = null;
                lsvChildSchedule.ItemsSource = dbr.GetSchedule(activeChild.Child_ID);
            }
        }

        private void btnAddTime_Click(object sender, RoutedEventArgs e)
        {
            CreateSchedule win = new CreateSchedule();
            win.Show();
            winCs.FuckYou();
        }
    }
}
