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
using BE;

namespace PLWPF.Windows
{
    /// <summary>
    /// Interaction logic for GuestRequestPage.xaml
    /// </summary>
    public partial class GuestRequestPage : Page
    {
        public GuestRequestPage(GuestRequest guestRequest)
        {
            InitializeComponent();
            PrivateName.Text = guestRequest.PrivateName;
            FamilyName.Text = guestRequest.FamilyName;
            MailAddress.Text = guestRequest.MailAddress;
            Status.Text = guestRequest.Status.ToString();
            RegistrationDate.Text = guestRequest.RegistrationDate.ToString();
            EntryDate.Text = guestRequest.EntryDate.ToString();
            ReleaseDate.Text = guestRequest.ReleaseDate.ToString();
            Area.Text = guestRequest.Area.ToString();
            SubArea.Text = guestRequest.SubArea;
            HostingType.Text = guestRequest.HostingType.ToString();
            Adults.Text = guestRequest.Adults.ToString();
            Children.Text = guestRequest.Children.ToString();
            Pool.Text = guestRequest.Pool.ToString();
            Jacuzzi.Text = guestRequest.Jacuzzi.ToString();
            Garden.Text = guestRequest.Garden.ToString();
            ChildrensAttractions.Text = guestRequest.ChildrensAttractions.ToString();
            SpredBads.Text = guestRequest.SpredBads.ToString();
            AirCondsner.Text = guestRequest.AirCondsner.ToString();
            frisider.Text = guestRequest.frisider.ToString();
            SingogNaerBy.Text = guestRequest.SingogNaerBy.ToString();
            NaerPublicTrensportion.Text = guestRequest.NaerPublicTrensportion.ToString();
        }
    }
}
