using BE;
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
    /// Interaction logic for HostUpdateWindow.xaml
    /// </summary>
    public partial class HostUpdateWindow : Window
    {

        public bool HostDeleted;
        Window mainWindow;
        public HostUpdateWindow(Host host,Window mainWindow)
        {
            HostDeleted = false;
            this.mainWindow = mainWindow;
            mainWindow.Hide();
            InitializeComponent();
            UpdateHostFrame.Content = new HostUserControl(host,false,this);
            DeleteHostFrame.Content = new HostUserControl(host,true,this);
        }

        private void Window_Closed(object sender, EventArgs e)
        {

                DialogResult = true;
                mainWindow.Visibility = Visibility.Visible;
        }
    }
}
