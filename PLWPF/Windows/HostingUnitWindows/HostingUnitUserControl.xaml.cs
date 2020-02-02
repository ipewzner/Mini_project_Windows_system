using BE;
using BL;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PLWPF.Windows.HostingUnitWindows
{

    /// <summary>
    /// Interaction logic for HostingUnitUserControl.xaml
    /// </summary>
    public partial class HostingUnitUserControl : UserControl
    {
        MyBl myBL = new MyBl();
        Host host;
        HostingUnit hostingUnit;
        bool newUnit, deleteUnit;

        /// <summary>
        /// c-tor
        /// </summary>
        /// <param name="host"></param>
        /// <param name="newUnit"></param>
        /// <param name="deleteUnit"></param>
        public HostingUnitUserControl(Host host, bool newUnit, bool deleteUnit)
        {
            this.newUnit = newUnit;
            this.deleteUnit = deleteUnit;
            InitializeComponent();

            //Initialize host 
            this.host = host;
            hostName.Text = host.FamilyName;
            hostId.Text = host.HostKey.ToString();

            //depend on what page you in
            button.Content = (deleteUnit == false) ? "Save" : "Delete";

            //if you add new unit you need to change the comboBox to TextBox
            if (newUnit == true)
            {
                HostingUnitKeyTextBox.Visibility = Visibility.Visible;
                HostingUnitKeyComboBox.Visibility = Visibility.Hidden;
                hostingUnit = new HostingUnit();
            }
            else
            {
                HostingUnitKeyTextBox.Visibility = Visibility.Hidden;
                HostingUnitKeyComboBox.Visibility = Visibility.Visible;

                HostingUnitKeyComboBox.ItemsSource = myBL.HustingUnitsBy(x => x.Owner.HostKey == host.HostKey);
                HostingUnitKeyComboBox.DisplayMemberPath = "HostingUnitName";
            }

            //ComboBox Items Source
            ComboBoxHostingType.ItemsSource= Enum.GetValues(typeof(BE.HostingType)).Cast<BE.HostingType>();
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

        /// <summary>
        /// Selection of Hosting Unit in the ComboBox Changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HostingUnitKeyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hostingUnit = HostingUnitKeyComboBox.SelectedItem as HostingUnit;
            InitialHostingUnit(hostingUnit);
        }

        /// <summary>
        /// Feel the Textbox & ComboBox with the Unit details
        /// </summary>
        /// <param name="unit"></param>
        private void InitialHostingUnit(HostingUnit unit)
        {
            ComboBoxHostingType.SelectedItem = unit.HostingType;
            Adults.Text = unit.Adults.ToString();
            Children.Text = unit.Children.ToString();
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

        /// <summary>
        /// button_Click will save the changes, or will delete the unit, depende the page. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (deleteUnit == true)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure\n?", "Delete unit", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        myBL.UnitRemove(hostingUnit.HostingUnitKey);
                            MessageBox.Show("Unit deleted");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Fail to delete unit \n " + ex);
                    }
                }
            }

            else
            {
                if (newUnit == true)
                {
                    hostingUnit.HostingUnitName = HostingUnitKeyTextBox.Text;
                    hostingUnit.Owner = host;
                }
                hostingUnit.HostingType = (HostingType)ComboBoxHostingType.SelectedItem;
                hostingUnit.Children = Int32.Parse(Children.Text);
                hostingUnit.Adults = Int32.Parse(Adults.Text);
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
                    if (newUnit == false)
                        myBL.UpdateUnit(hostingUnit);
                    else
                        myBL.AddHostingUnit(hostingUnit);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }

            }
        }

        /// <summary>
        /// Cancel the Changes thet you do
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelChanges_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (newUnit == true)
            {
                HostingUnitKeyTextBox.Clear();
                ComboBoxHostingType.SelectedItem = null;
                Adults.Text = null;
                Children.Text = null;
                AreaComboBox.SelectedItem = null;
                SubArea.Text = null;
                ComboBoxPool.SelectedItem = null;
                ComboBoxJacuzzi.SelectedItem = null;
                ComboBoxAttrac.SelectedItem = null;
                ComboBoxSpredBads.SelectedItem = null;
                ComboBoxAirCondsner.SelectedItem = null;
                ComboBoxGarden.SelectedItem = null;
                ComboBoxSingog.SelectedItem = null;
                ComboBoxTrensp.SelectedItem = null;
            }
            else
            {
                InitialHostingUnit(hostingUnit);
            }
        }
    }
}