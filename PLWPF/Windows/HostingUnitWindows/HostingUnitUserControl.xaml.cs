using BE;
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

            //depend on which page you in
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
            ComboBoxPool.ItemsSource = Enum.GetValues(typeof(BE.UnitRequirements)).Cast<BE.UnitRequirements>();
            ComboBoxJacuzzi.ItemsSource = Enum.GetValues(typeof(BE.UnitRequirements)).Cast<BE.UnitRequirements>();
            ComboBoxAttrac.ItemsSource = Enum.GetValues(typeof(BE.UnitRequirements)).Cast<BE.UnitRequirements>();
            ComboBoxSpredBads.ItemsSource = Enum.GetValues(typeof(BE.UnitRequirements)).Cast<BE.UnitRequirements>();
            ComboBoxAirCondsner.ItemsSource = Enum.GetValues(typeof(BE.UnitRequirements)).Cast<BE.UnitRequirements>();
            ComboBoxGarden.ItemsSource = Enum.GetValues(typeof(BE.UnitRequirements)).Cast<BE.UnitRequirements>();
            ComboBoxSingog.ItemsSource = Enum.GetValues(typeof(BE.UnitRequirements)).Cast<BE.UnitRequirements>();
            ComboBoxTrensp.ItemsSource = Enum.GetValues(typeof(BE.UnitRequirements)).Cast<BE.UnitRequirements>();
           
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
        /// Fills the Textbox & ComboBox with the Unit details
        /// </summary>
        /// <param name="unit"></param>
        private void InitialHostingUnit(HostingUnit unit)
        {
            ComboBoxHostingType.SelectedItem = unit.HostingType;
            Adults.Text = unit.Adults.ToString();
            Children.Text = unit.Children.ToString();
            AreaComboBox.SelectedItem = unit.Area;
            SubArea.Text = unit.SubArea;
            ComboBoxPool.SelectedItem = (UnitRequirements)unit.Pool;
            ComboBoxJacuzzi.SelectedItem = (UnitRequirements)unit.Jacuzzi;
            ComboBoxAttrac.SelectedItem = (UnitRequirements)unit.ChildrensAttractions;
            ComboBoxSpredBads.SelectedItem = (UnitRequirements)unit.SpredBads;
            ComboBoxAirCondsner.SelectedItem = (UnitRequirements)unit.AirCondsner;
            ComboBoxGarden.SelectedItem =(UnitRequirements)unit.Garden;
            ComboBoxSingog.SelectedItem =(UnitRequirements)unit.SingogNaerBy;
            ComboBoxTrensp.SelectedItem = (UnitRequirements)unit.NaerPublicTrensportion;

           // NaerByTextBox.Text = ((UnitRequirements)hostingUnit.SingogNaerBy).ToString();
          
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
                        MessageBox.Show("Fail to delete unit");
                        Console.WriteLine("Fail to delete unit \n " + ex);
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
                hostingUnit.Pool = (GestRequirements)ComboBoxPool.SelectedItem;
                hostingUnit.Jacuzzi = (GestRequirements)ComboBoxJacuzzi.SelectedItem;
                hostingUnit.ChildrensAttractions = (GestRequirements)ComboBoxAttrac.SelectedItem;
                hostingUnit.SpredBads = (GestRequirements)ComboBoxSpredBads.SelectedItem;
                hostingUnit.AirCondsner = (GestRequirements)ComboBoxAirCondsner.SelectedItem;
                hostingUnit.Garden = (GestRequirements)ComboBoxGarden.SelectedItem;
                hostingUnit.SingogNaerBy = (GestRequirements)ComboBoxSingog.SelectedItem;
                hostingUnit.NaerPublicTrensportion = (GestRequirements)ComboBoxTrensp.SelectedItem;

                if (newUnit == true)
                {
                    myBL.AddHostingUnit(hostingUnit);
                    MessageBox.Show("Adding Hosting Unit Secsessfully!");
                }
                else
                {
                    try
                    {
                        myBL.UpdateUnit(hostingUnit);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"cannot add or update {hostingUnit.HostingUnitName}");
                        Console.WriteLine(ex.Message);
                    }
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