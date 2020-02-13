using BE;
using System;
using System.Windows;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostUpdateWindow.xaml
    /// </summary>
    public partial class HostUpdateWindow : Window
    {

        public bool HostDeleted { set; get; }
        HostUserControl c;

        Window mainWindow;
        public HostUpdateWindow(Host host,Window mainWindow)
        {

            this.mainWindow = mainWindow;
            mainWindow.Hide();
            InitializeComponent();
            UpdateHostFrame.Content = new HostUserControl(host,false,this);
            c= new HostUserControl(host, true, this);
            DeleteHostFrame.Content = c;

        }
        
        private void Window_Closed(object sender, EventArgs e)
        {
            HostDeleted = c.HostDeleted  ;
            mainWindow.Visibility = Visibility.Visible;
        }
    }
}
