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
        HostUserControl hostUserControl;

        Window mainWindow;
        public HostUpdateWindow(Host host,Window mainWindow)
        {

            this.mainWindow = mainWindow;
            mainWindow.Hide();
            InitializeComponent();
            UpdateHostFrame.Content = new HostUserControl(host,false,this);
            hostUserControl = new HostUserControl(host, true, this);
            DeleteHostFrame.Content = hostUserControl;

        }
        
        private void Window_Closed(object sender, EventArgs e)
        {
            HostDeleted = hostUserControl.HostDeleted  ;
            mainWindow.Visibility = Visibility.Visible;
        }
    }
}
