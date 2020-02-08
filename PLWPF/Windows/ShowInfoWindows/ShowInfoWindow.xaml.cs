using System.Windows;

namespace PLWPF.Windows.ShowInfoWindows
{
    /// <summary>
    /// Interaction logic for ShowInfoWindow.xaml
    /// </summary>
    public partial class ShowInfoWindow : Window
    {
        public ShowInfoWindow()
        {
            InitializeComponent();
            GuestRequestPage.Content = new RequestInfoPage();
            HostPage.Content = new HostInfoPage();
            UnitPage.Content = new UnitInfoPage();
            OrdersPage.Content = new OrderInfoPage();
        }
    }
}
