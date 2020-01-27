using BL;
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

namespace PLWPF.Windows
{
    /// <summary>
    /// Interaction logic for NewOrderWindow.xaml
    /// </summary>
    public partial class NewOrderWindow : Window
    {
        MyBl myBL = new MyBl();
        Host host;
        public NewOrderWindow(Host host )
        {
            InitializeComponent();
            BE.Order order = new BE.Order();
            this.host= host;
            hostName.Text = host.FamilyName;
            hostId.Text = host.HostKey.ToString();

            guestRequestComboBox.ItemsSource = myBL.GuestRequestBy();
            guestRequestComboBox.DisplayMemberPath = "GuestRequestKey";
            
            //guestRequestComboBox.SelectedItem = "{Binding Path=GuestRequestKey}";

            HostingUnitKeyComboBox.ItemsSource = myBL.HustingUnitsBy(x=>x.Owner.HostKey==host.HostKey);
            HostingUnitKeyComboBox.DisplayMemberPath = "HostingUnitName";

        }

        /// <summary>
        /// Show the current guest request
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GuestRequest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {                 
            Right.Content = new GuestRequestPage(guestRequestComboBox.SelectedItem as GuestRequest);
        }
                                                            
        /// <summary>
        /// Show the current husting unit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HostingUnitKeyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                Left.Content = new HostingUnitPage(HostingUnitKeyComboBox.SelectedItem as HostingUnit);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Order order = new Order()
            {
                CreateDate = DateTime.Now,
                GuestRequestKey = (guestRequestComboBox.SelectedItem as GuestRequest).GuestRequestKey,
                HostingUnitKey = (HostingUnitKeyComboBox.SelectedItem as HostingUnit).HostingUnitKey,
                OrderDate = DateTime.Now,
                Status = OrderStatus.MailSent,
                OrderKey = Configuration.serialOrder++

            };

            try
            {
                myBL.AddOrder(order);
                try
                {
                    myBL.SendMail(order);
                }
                catch(Exception ex)
                {
                    throw new Exception("Can't sand mail ", ex);
                }
              MessageBox.Show("Order Created Seccessfuly!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! please try again later "+ex);
                this.Close();
            }

        }
    }
}
