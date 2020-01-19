using System;
using System.Collections.Generic;
using System.Globalization;
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
using BE;
using BL;

namespace PLWPF.Windows
{
    /// <summary>
    /// Interaction logic for HostingUnitPage.xaml
    /// </summary>
    public partial class HostingUnitPage : Page
    {
        public HostingUnitPage(HostingUnit hostingUnit)
        {
            
            InitializeComponent();
            // Clendar.BlackoutDates.Add(new CalendarDateRange(new DateTime(01 / 01 / 2020), new DateTime(01 / 01 / 2021)));
            //System.Windows.Controls.Calendar Calendar1 = new System.Windows.Controls.Calendar();
            //Calendar1.IsTodayHighlighted = true;
            //Calendar1.DisplayDate = new DateTime(2009, 1, 1);
            SelectedDatesCollection slected = calendarHU.SelectedDates;
            // calendarHU.BlackoutDates.Add(new CalendarDateRange(slected.ElementAt(0), slected.ElementAt(1)) );
            /*
                 
            public GuestRequestPage(GuestRequest guestRequest)
     {
         InitializeComponent();
         PrivateName.Text = guestRequest.PrivateName;
         Family
         
             */
            MyBl newBL = new MyBl();

            //foreach (var item in newBL.UintsAvailable())
            //{
            //    calendarHU.BlackoutDates.Add(item.);
            //}
            
            adultsTextBox.Text = hostingUnit.Adults.ToString();
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
            //subAreaTextBox.Text = hostingUnit.SubArea.ToString();
        }
    }  
}
