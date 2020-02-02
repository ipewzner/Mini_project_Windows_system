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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Password.xaml
    /// </summary>
    public partial class Password : UserControl
    {

        /// <summary>
        /// c-tor
        /// </summary>
        public Password()
        {
            InitializeComponent();
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
            ShowPassword.Content = "Hide";
            PasswordUnmask.Visibility = Visibility.Visible;
            PasswordHidden.Visibility = Visibility.Hidden;
            PasswordUnmask.Text = PasswordHidden.Password;
        }

        /// <summary>
        /// make the password unvisible
        /// </summary>
        private void HidePasswordFunction()
        {
            ShowPassword.Content = "Show";
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
            PasswordUnmask.Text = PasswordHidden.Password;
        }

        #endregion ShowPassword
    }
}
