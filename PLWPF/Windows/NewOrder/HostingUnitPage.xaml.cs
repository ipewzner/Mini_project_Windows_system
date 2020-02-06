using System.Windows.Controls;
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

            foreach (var item in hostingUnit.Diary)
            {
                CalendarDateRange x = new CalendarDateRange(item);
                calendarHU.BlackoutDates.Add(x);
                
            }

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
            airCondsnerTextBox.Text = ((UnitRequirements)hostingUnit.AirCondsner).ToString();
            areaTextBox.Text = hostingUnit.Area.ToString();
            childrenTextBox.Text = ((UnitRequirements)hostingUnit.Children).ToString();
            childrensAttractionsTextBox.Text = ((UnitRequirements)hostingUnit.ChildrensAttractions).ToString();
            frisiderTextBox.Text = ((UnitRequirements)hostingUnit.frisider).ToString();
            gardenTextBox.Text = ((UnitRequirements)hostingUnit.Garden).ToString();
            hostingTypeTextBox.Text = ((UnitRequirements)hostingUnit.HostingType).ToString();
            jacuzziTextBox.Text = ((UnitRequirements)hostingUnit.Jacuzzi).ToString();
            naerPublicTrensportionTextBox.Text = ((UnitRequirements)hostingUnit.NaerPublicTrensportion).ToString();
            poolTextBox.Text = ((UnitRequirements)hostingUnit.Pool).ToString();
            singogNaerByTextBox.Text = ((UnitRequirements)hostingUnit.SingogNaerBy).ToString();
            spredBadsTextBox.Text = ((UnitRequirements)hostingUnit.SpredBads).ToString();
           // subAreaTextBox.Text = hostingUnit.SubArea.ToString();
        }
    }  
}
