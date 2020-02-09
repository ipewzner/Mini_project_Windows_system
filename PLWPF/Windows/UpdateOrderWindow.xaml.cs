using BE;
using BL;
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
    /// Interaction logic for UpdateOrderWindow.xaml
    /// </summary>
    public partial class UpdateOrderWindow : Window
    {
        MyBl myBL = new MyBl();
        Order order = new Order();
        Host host;

        /// <summary>
        /// c-tor
        /// </summary>
        /// <param name="hostKey"></param>
        public UpdateOrderWindow(Host host)
        {
            this.host = host;
            InitializeComponent();
            var source = myBL.GetOrders(x => myBL.GetHostingUnit(x.HostingUnitKey).Owner.HostKey == host.HostKey);
            if (source != null)
            {
                comboBox.ItemsSource = source;
                comboBox.DisplayMemberPath = "OrderKey";

                StatusComboBox.ItemsSource = Enum.GetValues(typeof(BE.OrderStatus)).Cast<BE.OrderStatus>();
                HostingUnitKeyComboBox.ItemsSource = myBL.GetHostingUnitsKeysList(host.HostKey);
            }
            else
            {
                MessageBox.Show("No orders to show");
                this.Close();
            }
        }

        /// <summary>
        /// finish order end close window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            order.Status = (OrderStatus)StatusComboBox.SelectedItem;
            myBL.UpdateOrder(order);
            this.Close();
        }
        
        private void OrderKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            order = myBL.GetOrders(x => x.OrderKey == Int32.Parse(OrderKey.Text)).FirstOrDefault();
            if (order != null)
            {
                GuestRequestKey.Text = order.GuestRequestKey.ToString();
                HostingUnitKeyComboBox.SelectedItem = (order.HostingUnitKey).ToString();
                CreateDate.Text = order.CreateDate.ToString();
                OrderDate.Text = order.OrderDate.ToString();
                StatusComboBox.Text = order.Status.ToString();
                GuestRequestKey.Text = order.GuestRequestKey.ToString();
                Continue.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("No Order faund!");
                Continue.Visibility = Visibility.Hidden;
            }
        }
        private void GuestRequestKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            //order = myBL.GetOrders(x => x.GuestRequestKey == Int32.Parse(GuestRequestKey.Text)).FirstOrDefault();
            //if (order != null)
            //{
            //    //HostingUnitKey.Text = (order.HostingUnitKey).ToString();
            //    CreateDate.Text = order.CreateDate.ToString();
            //    OrderDate.Text = order.OrderDate.ToString();
            //    StatusComboBox.Text = order.Status.ToString();
            //    OrderKey.Text = order.OrderKey.ToString();
            //    Continue.Visibility = Visibility.Visible;

            //}
            //else
            //{
            //    MessageBox.Show("No Order faund!");
            //   // Continue.Visibility = Visibility.Hidden;
            //}
        }
          

        /// <summary>
        /// Status changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_StatusChanged(object sender, SelectionChangedEventArgs e)
        {
            try {
                Enum.TryParse(StatusComboBox.SelectedItem.ToString(), out OrderStatus OrderStatus);
                order.Status = OrderStatus;
            }         
            catch
            {        
                 MessageBox.Show("No Order faund!");
                StatusComboBox.Text = null;
            }
        }

        private void ComboBox_HostingUnitChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
               // StatusComboBox.SelectedItem.ToString();
                
            }
            catch
            {
                MessageBox.Show("No Order faund!");
                StatusComboBox.Text = null;
            }
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            order = myBL.GetOrders(x => x.OrderKey == (comboBox.SelectedItem as Order).OrderKey).FirstOrDefault();
            if (order != null)
            {
                GuestRequestKey.Text = order.GuestRequestKey.ToString();
                HostingUnitKeyComboBox.Text = order.HostingUnitKey.ToString();
                CreateDate.Text = order.CreateDate.ToString();
                OrderDate.Text = order.OrderDate.ToString();
                StatusComboBox.Text = order.Status.ToString();
                OrderKey.Text = order.OrderKey.ToString();
                Continue.Visibility = Visibility.Visible;

            }
            else
            {
                MessageBox.Show("No Order faund!");
                // Continue.Visibility = Visibility.Hidden;
            }
        }
    }
}
//maybe singlton is needed    for thos to func not intersect "OrderKey_TextChanged" &"GuestRequestKey_TextChanged"