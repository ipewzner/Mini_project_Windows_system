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
        Window mainWindow;
        public HostingUnitWindow(Host host,Window mainWindow)
        {
           this.mainWindow = mainWindow;
            mainWindow.Hide();
            InitializeComponent();
            AddNewUnitFrame.Content = new HostingUnitUserControl(host,true, false,this);
            UpdateUnitFrame.Content = new HostingUnitUserControl(host, false, false, this);
            DeleteUnitFrame.Content = new HostingUnitUserControl(host, false, true, this);
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            mainWindow.Visibility = Visibility.Visible;
        }
    }
}
