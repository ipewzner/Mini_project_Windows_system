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
using BE;
using BL;
using PLWPF.Windows;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
           
        MyBl myBL = new MyBl();
        Host host;
        public Host HostfromLogin { get => host; }
        public LogInWindow()
        {
            InitializeComponent();
            HostComboBox.ItemsSource = myBL.GetHosts();
            HostComboBox.DisplayMemberPath = "FamilyName";
            /*
            HostKey 
            PrivateName  
            FamilyName            
            */
        }

        /// <summary>
        /// selcted host
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Host_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            host = HostComboBox.SelectedItem as Host;
        }

        

        private void Password_TextChanged(object sender, TextChangedEventArgs e)
        {
           // int pass  = Int32.Parse(Password.Text);
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Window win = new Register();
            win.Show();
        }

        private void ENTER_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
