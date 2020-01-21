using BE;
using BL;
using PLWPF.Windows;
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
    /// Interaction logic for HostingUnitWindow.xaml
    /// </summary>
    public partial class HostingUnitWindow : Window
    {
        MyBl myBL = new MyBl();
        Host host;
        HostingUnit hostingUnit;

        public HostingUnitWindow(Host host)
        {
            InitializeComponent();
            this.host = host;
            hostName.Text = host.FamilyName;
            hostId.Text = host.HostKey.ToString();
            HostingUnitKeyComboBox.ItemsSource = myBL.HustingUnitsBy(x => x.Owner.HostKey == host.HostKey);
            HostingUnitKeyComboBox.DisplayMemberPath = "HostingUnitName";


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


        private void HostingUnitKeyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hostingUnit = HostingUnitKeyComboBox.SelectedItem as HostingUnit;
            InitialHostingUnit(hostingUnit);
        }

        private void InitialHostingUnit(HostingUnit unit)
        {
            AreaComboBox.SelectedItem = unit.Area;
            SubArea.Text = unit.SubArea;
            ComboBoxPool.SelectedItem = unit.Pool;
            ComboBoxJacuzzi.SelectedItem = unit.Jacuzzi;
            ComboBoxAttrac.SelectedItem = unit.ChildrensAttractions;
            ComboBoxSpredBads.SelectedItem = unit.SpredBads;
            ComboBoxAirCondsner.SelectedItem = unit.AirCondsner;
            ComboBoxGarden.SelectedItem = unit.Garden;
            ComboBoxSingog.SelectedItem = unit.SingogNaerBy;
            ComboBoxTrensp.SelectedItem = unit.NaerPublicTrensportion;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            hostingUnit.SubArea = SubArea.Text;
            hostingUnit.Area = (Area)AreaComboBox.SelectedItem;
            hostingUnit.Pool = (Requirements)ComboBoxPool.SelectedItem;
            hostingUnit.Jacuzzi = (Requirements)ComboBoxJacuzzi.SelectedItem;
            hostingUnit.ChildrensAttractions = (Requirements)ComboBoxAttrac.SelectedItem;
            hostingUnit.SpredBads = (Requirements)ComboBoxSpredBads.SelectedItem;
            hostingUnit.AirCondsner = (Requirements)ComboBoxAirCondsner.SelectedItem;
            hostingUnit.Garden = (Requirements)ComboBoxGarden.SelectedItem;
            hostingUnit.SingogNaerBy = (Requirements)ComboBoxSingog.SelectedItem;
            hostingUnit.NaerPublicTrensportion = (Requirements)ComboBoxTrensp.SelectedItem;

            try
            {
                myBL.UnitRemove(hostingUnit.HostingUnitKey);
                myBL.AddHostingUnit(hostingUnit);
                MessageBox.Show($"Hosting unit: {hostingUnit.HostingUnitName} updated Seccessfuly!");
                this.Close();

            }
            catch (Exception)
            {
                MessageBox.Show($"Error during Update {hostingUnit.HostingUnitName} please try again later!");
                this.Close();

            }


        }
    }
}
