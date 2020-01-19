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

        }



        /// <summary>
        /// Show the current husting unit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HostingUnitKeyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HustingUnitFrame.Content = new HostingUnitPage(HostingUnitKeyComboBox.SelectedItem as HostingUnit);
            hostingUnit = HostingUnitKeyComboBox.SelectedItem as HostingUnit;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {

            hostingUnit. Adults                        = ((HostingUnit)HustingUnitFrame.Content).Adults;
            hostingUnit. AirCondsner                   = ((HostingUnit)HustingUnitFrame.Content)  .  AirCondsner                    ;
            hostingUnit. Area                          = ((HostingUnit)HustingUnitFrame.Content)  .  Area                           ;
             hostingUnit.Children                      = ((HostingUnit)HustingUnitFrame.Content)   .Children                        ;
             hostingUnit.    ChildrensAttractions      = ((HostingUnit)HustingUnitFrame.Content)   .    ChildrensAttractions        ;
             hostingUnit.frisider                      = ((HostingUnit)HustingUnitFrame.Content)   .frisider                        ;
             hostingUnit.Garden                        = ((HostingUnit)HustingUnitFrame.Content)   .Garden                          ;
             hostingUnit.HostingType                   = ((HostingUnit)HustingUnitFrame.Content)   .HostingType                     ;
             hostingUnit.Jacuzzi                       = ((HostingUnit)HustingUnitFrame.Content)   .Jacuzzi                         ;
             hostingUnit.NaerPublicTrensportion        = ((HostingUnit)HustingUnitFrame.Content)   .NaerPublicTrensportion          ;
             hostingUnit.Pool                          = ((HostingUnit)HustingUnitFrame.Content)   .Pool                            ;
             hostingUnit.SingogNaerBy                  = ((HostingUnit)HustingUnitFrame.Content)   .SingogNaerBy                    ;
             hostingUnit.SpredBads                     = ((HostingUnit)HustingUnitFrame.Content). SpredBads                         ;
            hostingUnit.SubArea = ((HostingUnit)HustingUnitFrame.Content).SubArea;
                /* adultsTextBox.Text = hostingUnit.Adults.ToString();
            airCondsnerTextBox.Text = hostingUnit.AirCondsner.ToString();
            areaTextBox.Text = hostingUnit.Area.ToString();
            childrenTextBox.Text = hostingUnit.Children.ToString();
            childrensAttractionsTextBox.Text = hostingUnit.ChildrensAttractions.ToString();
            frisiderTextBox.Text = hostingUnit.frisider.ToString();
            gardenTextBox.Text = hostingUnit.Garden.ToString();
            hostingTypeTextBox.Text = hostingUnit.HostingType.ToString();
            jacuzziTextBox.Text = hostingUnit.Jacuzzi.ToString();
            naerPublicTrensportionTextBox.Text = hostingUnit.NaerPublicTrensportion.ToString();
            poolTextBox.Text = hostingUnit.Pool.ToString();
            singogNaerByTextBox.Text = hostingUnit.SingogNaerBy.ToString();
            spredBadsTextBox.Text = hostingUnit.SpredBads.ToString();
            subAreaTextBox.Text = hostingUnit.SubArea.ToString();
            */
        }

        private void HustingUnitFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
    }
}
