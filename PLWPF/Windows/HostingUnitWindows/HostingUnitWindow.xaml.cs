using BE;
using PLWPF.Windows.HostingUnitWindows;
using System.Windows;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostingUnitWindow.xaml
    /// </summary>
    public partial class HostingUnitWindow : Window
    {
        public HostingUnitWindow(Host host)
        {
            InitializeComponent();
            AddNewUnitFrame.Content = new HostingUnitUserControl(host,true, false);
            UpdateUnitFrame.Content = new HostingUnitUserControl(host, false, false);
            DeleteUnitFrame.Content = new HostingUnitUserControl(host, false, true);
        }
    }
}
