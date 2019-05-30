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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;

namespace DatabasDagcentretRenen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow AppWindow;
        dbOpsRead db;

        List<Klasser_tabeller.PresentChildrenForStaff> pc = new List<Klasser_tabeller.PresentChildrenForStaff>();
        public Klasser_tabeller.PresentChildrenForStaff activePc;
        public Klasser_tabeller.Staff activeStaff;


        public MainWindow()
        {
            InitializeComponent();
            db = new dbOpsRead();
            FillCombo();
            AppWindow = this;
        }

        public Klasser_tabeller.PresentChildrenForStaff GetChild ()
        {
            return activePc;
        }
        public Klasser_tabeller.Staff GetStaff()
        {
            return activeStaff;
        }

        public void FillListView()
        {
            if (activeStaff != null)
            {
                string date = dateP.ToString();
                int id = activeStaff.Staff_id;
                lvChildrenStaff.ItemsSource = null;
                lvChildrenStaff.ItemsSource = db.GetTodaysKids(id, date);
            }
          
        }
        public void FillBreakfast()
        {
            if (activeStaff != null)
            {
                int numberOfBreakfast;
                string date = dateP.ToString();
                numberOfBreakfast = db.NumberOfBreakfasts(date);
                lblBreakfast.Content = numberOfBreakfast.ToString() + " barn ska ha frukost idag";
                lstAllergy.ItemsSource = null;
                lstAllergy.ItemsSource = db.AllergyList(date);
            } 
        }

        public void FillCombo()
        {
            cmbStaff.ItemsSource = db.GetStaff();
        }

        private void cmbStaff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            activeStaff = (Klasser_tabeller.Staff)cmbStaff.SelectedItem;
            FillListView();
            FillBreakfast();
        }

        private void dateP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FillListView();
            FillBreakfast();
        }

        private void lvChildrenStaff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            activePc = (Klasser_tabeller.PresentChildrenForStaff)lvChildrenStaff.SelectedItem;
        }

        private void lvChildrenStaff_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MsgBoxAbsence msg = new MsgBoxAbsence();
            msg.Show();
        }

        private void btnAllKids_Click(object sender, RoutedEventArgs e)
        {
            string date = dateP.ToString();
            int id = 0;
            int numberOfBreakfast;
            lvChildrenStaff.ItemsSource = db.GetTodaysKids(id, date);
            numberOfBreakfast = db.NumberOfBreakfasts(date);
            lblBreakfast.Content = numberOfBreakfast.ToString() + " barn ska ha frukost idag";
            lstAllergy.ItemsSource = null;
            lstAllergy.ItemsSource = db.AllergyList(date);
        }

        private void btnAStaffsKids_Click(object sender, RoutedEventArgs e)
        {
            FillListView();
            FillBreakfast();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (activePc != null)
            {
                childpagefromstaff chw = new childpagefromstaff();
                chw.Show();
            }
        }

        private void btnChangeGame_Click(object sender, RoutedEventArgs e)
        {
            VårdnadshavareMainWin winVh = new VårdnadshavareMainWin();
            
            winVh.Show();
            this.Hide();
        }
    }
}
