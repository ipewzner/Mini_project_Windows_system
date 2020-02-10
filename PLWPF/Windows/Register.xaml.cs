using System.Windows;

namespace PLWPF.Windows
{

    public partial class Register : Window
    {
       
        public Register()
        {
            InitializeComponent();
            Host_UserControl.Content = new HostUserControl(null,false,this);
          
        }
    }
}

        
 
