using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        ///  The event selcte the host
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Host_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ENTER_Button.IsEnabled = true;
            ForgetYourPassword.IsEnabled = true;
            host = HostComboBox.SelectedItem as Host;
        }

        /// <summary>
        /// The event call the Register window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Window win = new Register();
            win.Show();
        }

        /// <summary>
        /// The event checkק the password and if it's correct close  the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ENTER_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (myBL.CheckePassword(host.PasswordKey, Int32.Parse(PasswordHidden.Password)))
                {
                    DialogResult = true;
                    Close();
                }
                else MessageBox.Show("Password incorrect!\n try again");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Problem with the password \n" + ex);
            }
        }


        BackgroundWorker worker = new BackgroundWorker();

        private void Worker_SendMailWithNewPassword(object sender, DoWorkEventArgs e)
        {
            myBL.SendMailWithNewPassword(host);
        }

        /// <summary>
        /// The event call to ShowPasswordFunction 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ForgetYourPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            worker.DoWork += Worker_SendMailWithNewPassword;
        
            if ( MessageBox.Show("Send you email \nwith new password?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                   
                    if (worker.IsBusy != true) worker.RunWorkerAsync();
                    MessageBox.Show("Email was sent!","Massage", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fail to send the Email\n" + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            

        }


        #region ShowPassword   

        /// <summary>
        /// The event call to ShowPasswordFunction 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e) => ShowPasswordFunction();

        /// <summary>
        /// The event call to HidePasswordFunction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e) => HidePasswordFunction();

        /// <summary>
        /// The event call to HidePasswordFunction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPassword_MouseLeave(object sender, MouseEventArgs e) => HidePasswordFunction();

        /// <summary>
        /// make the password visible
        /// </summary>
        private void ShowPasswordFunction()
        {
            ShowPassword.Text = "HIDE";
            PasswordUnmask.Visibility = Visibility.Visible;
            PasswordHidden.Visibility = Visibility.Hidden;
            PasswordUnmask.Text = PasswordHidden.Password;
        }

        /// <summary>
        /// make the password unvisible
        /// </summary>
        private void HidePasswordFunction()
        {
            ShowPassword.Text = "SHOW";
            PasswordUnmask.Visibility = Visibility.Hidden;
            PasswordHidden.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// update the password unmask
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            lll.Content = PasswordHidden.Password;
            PasswordUnmask.Text = PasswordHidden.Password;
        }
       
        #endregion ShowPassword
    }
}
