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
            ChoiseComboBox1.ItemsSource = Enum.GetValues(typeof(BE.Requirements)).Cast<BE.Requirements>();

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GuestRequest guest = new GuestRequest();
            guest.PrivateName = FirstName.Text;
            guest.FamilyName = LastName.Text;

            Enum.TryParse(AreaComboBox.SelectedItem.ToString(), out Area area);
            guest.Area = area;

            guest.EntryDate = EntryDate.DisplayDate;
            guest.ReleaseDate = ReleaseDate.DisplayDate;
            guest.SubArea = SubArea.Text;

            Enum.TryParse(AreaComboBox.SelectedItem.ToString(), out Requirements pool);
            guest.Pool =pool;

            try
            {
                myBL.AddGuestRequest(guest);
                MessageBox.Show("Recived Seccessfully");
            }
            catch (Exception)
            {
                MessageBox.Show("Fuck that shit!");
            }

        }
    }
}
