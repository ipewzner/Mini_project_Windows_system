using BL;
using BE;
using System;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace PLWPF.Windows
{
    /// <summary>
    /// Interaction logic for NewOrderWindow.xaml
    /// </summary>
    public partial class NewOrderWindow : Window
    {
        MyBl myBL = new MyBl();
        Host host;

        /// <summary>
        /// c-tor
        /// </summary>
        /// <param name="host"></param>
        public NewOrderWindow(Host host )
        {
            InitializeComponent();
            Order order = new Order();
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

        BackgroundWorker worker = new BackgroundWorker();

        private void Worker_SendMailWithNewOffer(object sender, DoWorkEventArgs e)
        {
            myBL.SendMail(e.Argument as Order);
        }

        /// <summary>
        /// Make Order send email and change status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {  try
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

            
                var check = myBL.AddOrder(order);
                if (check)
                {
                    worker.DoWork += Worker_SendMailWithNewOffer;
                    worker.RunWorkerAsync(order);
                    try
                    {
                        if (worker.IsBusy != true) worker.RunWorkerAsync();
                        MessageBox.Show("Email was sent!", "Massage", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Fail to send the Email\n", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    try
                    {
                        myBL.SendMail(order);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Can't sand mail");
                        Console.WriteLine("Can't sand mail" + ex.Message);
                    }
                    MessageBox.Show("Order Created Seccessfuly!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No Aviable Rooms For This Date!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! please try again later");
                Console.WriteLine(ex.Message);
                this.Close();
            }

        }
    }
}
