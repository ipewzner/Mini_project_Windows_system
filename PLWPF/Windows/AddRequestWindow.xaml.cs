using BE;
using BL;
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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddRequestWindow.xaml
    /// </summary>
    public partial class AddRequestWindow : Window
    {
        MyBl myBL = new MyBl();

        public AddRequestWindow()
        {
            InitializeComponent();
            AreaComboBox.ItemsSource = Enum.GetValues(typeof(BE.Area)).Cast<BE.Area>();
            ComboBoxPool.ItemsSource = Enum.GetValues(typeof(BE.Requirements)).Cast<BE.Requirements>();
            ComboBoxJacuzzi.ItemsSource = Enum.GetValues(typeof(BE.Requirements)).Cast<BE.Requirements>();
            ComboBoxAttrac.ItemsSource = Enum.GetValues(typeof(BE.Requirements)).Cast<BE.Requirements>();
            ComboBoxSpredBads.ItemsSource = Enum.GetValues(typeof(BE.Requirements)).Cast<BE.Requirements>();
            ComboBoxAirCondsner.ItemsSource = Enum.GetValues(typeof(BE.Requirements)).Cast<BE.Requirements>();
            ComboBoxGarden.ItemsSource = Enum.GetValues(typeof(BE.Requirements)).Cast<BE.Requirements>();
            ComboBoxSingog.ItemsSource = Enum.GetValues(typeof(BE.Requirements)).Cast<BE.Requirements>();
            ComboBoxTrensp.ItemsSource = Enum.GetValues(typeof(BE.Requirements)).Cast<BE.Requirements>();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GuestRequest guest = new GuestRequest();
                guest.PrivateName = FirstName.Text;
                guest.FamilyName = LastName.Text;

                guest.EntryDate = EntryDate.DisplayDate;
                guest.ReleaseDate = ReleaseDate.DisplayDate;
                guest.SubArea = SubArea.Text;
                guest.MailAddress = mailAddress.Text;
                guest.Area = (Area)AreaComboBox.SelectedItem;
                guest.Pool = (Requirements)ComboBoxPool.SelectedItem;
                guest.Jacuzzi = (Requirements)ComboBoxJacuzzi.SelectedItem;
                guest.ChildrensAttractions = (Requirements)ComboBoxAttrac.SelectedItem;
                guest.SpredBads = (Requirements)ComboBoxSpredBads.SelectedItem;
                guest.AirCondsner = (Requirements)ComboBoxAirCondsner.SelectedItem;
                guest.Garden = (Requirements)ComboBoxGarden.SelectedItem;
                guest.SingogNaerBy = (Requirements)ComboBoxSingog.SelectedItem;
                guest.NaerPublicTrensportion = (Requirements)ComboBoxTrensp.SelectedItem;


                myBL.AddGuestRequest(guest);
                MessageBox.Show("Recived Seccessfully");
            }
            catch (Exception)
            {
                MessageBox.Show("Error! Make sure you dont miss any field!");
            }

        }
    }
}