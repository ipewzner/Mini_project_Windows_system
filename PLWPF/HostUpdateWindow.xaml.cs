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
        public HostUpdateWindow(Host host)
        {
            InitializeComponent();
            UpdateHostFrame.Content = new HostUserControl(host,false,this);
            DeleteHostFrame.Content = new HostUserControl(host,true,this);
        }
    }
}
