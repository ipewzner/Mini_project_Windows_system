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

namespace PLWPF.Windows
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        MyBl myBL = new MyBl();

        public Register()
        {
            InitializeComponent();

        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Host host = new Host();
                BankAccount bankAccount = new BankAccount();

                host.PrivateName = PrivateName.Text;
                host.FamilyName = FamilyName.Text;
                host.PhoneNumber = PhoneNumber.Text;
                host.MailAddress = MailAddress.Text;

                bankAccount.BankName = Bank_ComboBox.Text;
                bankAccount.BranchCity = BranchCity_ComboBox.Text;
                bankAccount.BranchAddress = BranchAddress_ComboBox.Text;
                bankAccount.BankNumber = Convert.ToInt32(BankNumber.Text);
                bankAccount.BranchNumber = Convert.ToInt32(BranchNumber.Text);
                bankAccount.BankAccountNumber = Convert.ToInt32(BankAccountNumber.Text);

                host.BankAccount = bankAccount;

                myBL.AddHost(host);
                MessageBox.Show("Recived Seccessfully");
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error! Make sure you dont miss any field!");
            }
        }
    }
}
