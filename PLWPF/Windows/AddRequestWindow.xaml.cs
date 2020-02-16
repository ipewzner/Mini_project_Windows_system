using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        /// <summary>
        /// c-tor
        /// </summary>
        public AddRequestWindow()
        {
            InitializeComponent();
            AreaComboBox.ItemsSource = Enum.GetValues(typeof(BE.Area)).Cast<BE.Area>();
            ComboBoxPool.ItemsSource = Enum.GetValues(typeof(BE.GestRequirements)).Cast<BE.GestRequirements>();
            ComboBoxJacuzzi.ItemsSource = Enum.GetValues(typeof(BE.GestRequirements)).Cast<BE.GestRequirements>();
            ComboBoxAttrac.ItemsSource = Enum.GetValues(typeof(BE.GestRequirements)).Cast<BE.GestRequirements>();
            ComboBoxSpredBads.ItemsSource = Enum.GetValues(typeof(BE.GestRequirements)).Cast<BE.GestRequirements>();
            ComboBoxAirCondsner.ItemsSource = Enum.GetValues(typeof(BE.GestRequirements)).Cast<BE.GestRequirements>();
            ComboBoxGarden.ItemsSource = Enum.GetValues(typeof(BE.GestRequirements)).Cast<BE.GestRequirements>();
            ComboBoxSingog.ItemsSource = Enum.GetValues(typeof(BE.GestRequirements)).Cast<BE.GestRequirements>();
            ComboBoxTrensp.ItemsSource = Enum.GetValues(typeof(BE.GestRequirements)).Cast<BE.GestRequirements>();
        }

        /// <summary>
        /// Add the Request
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = EntryDate.SelectedDate ?? DateTime.Now;
            DateTime endDate = ReleaseDate.SelectedDate ?? DateTime.Now;

            if(startDate < DateTime.Now)
            {
                MessageBox.Show("can't order ealier date than today");
            }

            else if (!myBL.IsDateCorrect(startDate, endDate))
            {
                MessageBox.Show("can't add this request, Release Date before Entry Date!");
            }

            else if (!IsValidEmail(mailAddress.Text))
            {
                MessageBox.Show("error in mail address format");
            }
            else if (!IsTextValid(FirstName.Text))
            {
                MessageBox.Show("error in First Name format");
            }
            else if (!IsTextValid(LastName.Text))
            {
                MessageBox.Show("error in Last Name format");
            }
            else if (!IsTextValid(SubArea.Text))
            {
                MessageBox.Show("error in Sub Area format");
            }
            else try
            {
                GuestRequest guest = new GuestRequest();
                guest.PrivateName = FirstName.Text;
                guest.FamilyName = LastName.Text;

                guest.EntryDate = startDate;
                guest.ReleaseDate = endDate;
                guest.SubArea = SubArea.Text;
                guest.MailAddress = mailAddress.Text;
                guest.Area = (Area)AreaComboBox.SelectedItem;
                guest.Pool = (GestRequirements)ComboBoxPool.SelectedItem;
                guest.Jacuzzi = (GestRequirements)ComboBoxJacuzzi.SelectedItem;
                guest.ChildrensAttractions = (GestRequirements)ComboBoxAttrac.SelectedItem;
                guest.SpredBads = (GestRequirements)ComboBoxSpredBads.SelectedItem;
                guest.AirCondsner = (GestRequirements)ComboBoxAirCondsner.SelectedItem;
                guest.Garden = (GestRequirements)ComboBoxGarden.SelectedItem;
                guest.SingogNaerBy = (GestRequirements)ComboBoxSingog.SelectedItem;
                guest.NaerPublicTrensportion = (GestRequirements)ComboBoxTrensp.SelectedItem;


                bool check = myBL.AddGuestRequest(guest);
                if(check)
                MessageBox.Show("Recived Seccessfully");
                    this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! Make sure you dont miss any field and !");
            }

        }

        /// <summary>
        /// Cancel change in the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelChanges_PreviewMouseDown(object sender, RoutedEventArgs e)
        {
            FirstName.Text = "";
            LastName.Text = "";
            EntryDate.Text = "";
            ReleaseDate.Text = "";
            SubArea.Text = "";
            mailAddress.Text = "";
            AreaComboBox.SelectedItem = null;
            ComboBoxPool.SelectedItem = null;
            ComboBoxJacuzzi.SelectedItem = null;
            ComboBoxAttrac.SelectedItem = null;
            ComboBoxSpredBads.SelectedItem = null;
            ComboBoxAirCondsner.SelectedItem = null;
            ComboBoxGarden.SelectedItem = null;
            ComboBoxSingog.SelectedItem = null;
            ComboBoxTrensp.SelectedItem = null;
        }

        private void EntryDate_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ReleaseDate.DisplayDateStart = EntryDate.DisplayDate;

            ReleaseDate.BringIntoView();

           // ReleaseDate.UpdateLayout();
        }

        public static bool IsValidEmail(string emailAddress)
        {
            var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            bool isValid = Regex.IsMatch(emailAddress, regex, RegexOptions.IgnoreCase);
            return isValid;
        }

        public static bool IsTextValid(string text)
        {
            var regex = "^[A-Z][a-zA-Z]*$";
            bool isValid = Regex.IsMatch(text, regex, RegexOptions.IgnoreCase);
            return isValid;
        }

        
    }
}