using BE;
using BL;
using PLWPF.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostUserControl.xaml
    /// </summary>
    public partial class HostUserControl : UserControl
    {
        MyBl myBL = new MyBl();
        bool newHost, deleteHost;
        Host host;
        BankAccount bankAccount;

        public HostUserControl(Host hosts, bool deleteHost)
        {
            InitializeComponent();
            DataContext = this;
            Banks = myBL.GetBanks();

            //if this not a new host
            if (hosts != null)
            {
                host = hosts;
                bankAccount = host.BankAccount;
                FillTheFeilds();
                this.deleteHost = deleteHost;
            }
            else { newHost = true; }

            ContinueButton.Content = (deleteHost==true)?"Delete":"Save";
        }

        private void FillTheFeilds()
        {
            FamilyName.Text= host.FamilyName;
            PrivateName.Text= host.PrivateName;
            PhoneNumber.Text= host.PhoneNumber ;
            MailAddress.Text= host.MailAddress ;
            
            Bank_ComboBox.SelectedItem= bankAccount.BankNumber;
            BranchCity.Text= bankAccount.BranchCity;
            //BranchAddress.Text= bankAccount.BranchAddress;
            BankNumber.Text= bankAccount.BankNumber             .ToString();
            BranchNumber_ComboBox.SelectedItem = bankAccount.BranchNumber.ToString();
            BankAccountNumber.Text=bankAccount.BankAccountNumber.ToString();
        }

        /// <summary>
        /// check password Match
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
                if ((FirstPassword.PasswordHidden.Password) != (SecundPassword.PasswordHidden.Password))
                {
                    passwordMatchError.Visibility = Visibility.Visible;
                }
                else
                {
                    passwordMatchError.Visibility = Visibility.Hidden;
                }
        }


        private void CancelChanges_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!newHost) FillTheFeilds();
            else {
                FamilyName.Text =null;
                PrivateName.Text = null;
                PhoneNumber.Text = null;
                MailAddress.Text = null;

                Bank_ComboBox.Text = null;
                BranchCity.Text =   null;
                //BranchAddress_ComboBox.Text =null;
                BankNumber.Text =            null;
                BranchNumber_ComboBox.Text = null;
                BankAccountNumber.Text = null;
            }
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidEmail(MailAddress.Text))
            {
                if (deleteHost)
                {
                    try { myBL.RemoveHost(host); }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to delete host");
                        Console.WriteLine("Unable to delete host" + ex.Message);
                    }
                    MessageBox.Show("Host remove seccessfully");
                }
                else
                {
                    try
                    {
                        if (newHost)
                        {
                            host = new Host();
                            bankAccount = new BankAccount();
                            //host.FamilyName = FamilyNameTextBox.Text;

                        }
                        host.FamilyName = FamilyName.Text;
                        host.PrivateName = PrivateName.Text;
                        host.PhoneNumber = PhoneNumber.Text;
                        host.MailAddress = MailAddress.Text;
                        if (FirstPassword.PasswordHidden.Password != null)
                        {
                            host.PasswordKey = myBL.KeyForPassword(Int32.Parse(FirstPassword.PasswordHidden.Password));
                        }

                        bankAccount.BankName = ((BankDetails)Bank_ComboBox.SelectedItem).BankName;
                        bankAccount.BranchCity = BranchCity.Text;
                        //bankAccount.BranchAddress = BranchAddress_ComboBox.Text;
                        bankAccount.BankNumber = Convert.ToInt32(BankNumber.Text);
                        bankAccount.BranchNumber = ((BankBranch)BranchNumber_ComboBox.SelectedItem).BranchNumber;
                        bankAccount.BankAccountNumber = Convert.ToInt32(BankAccountNumber.Text);

                        host.BankAccount = bankAccount;

                        if (newHost)
                        {
                            if (myBL.AddHost(host))
                            {
                                MessageBox.Show("Recived Seccessfully");

                            }

                            else MessageBox.Show("Error! Make sure you dont miss any field!");
                        }
                        else
                        {
                            try { myBL.UpdateHost(host); }
                            catch (Exception ex)
                            {

                                MessageBox.Show("Unable to update host");
                                Console.WriteLine("Unable to update host" + ex.Message);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Fail!");
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Error in mail format please try again");
            }

        }

        public IEnumerable<BankDetails> Banks { get; set; }
        public string selectedBank { get; set; }

       // public ObservableCollection<BankBranch> Branches { get; set; }

        //public IEnumerable<BankBranch> Branches
        //{
        //    get { return _Branches; }
        //    set
        //    {
        //        if (_Branches == null)
        //        {
        //            _Branches = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}


        //public event PropertyChangedEventHandler PropertyChanged;

        //private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}



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
                bankAccount.BranchCity = BranchCity.Text;
                //bankAccount.BranchAddress = Convert.ToInt32(.Text);
                bankAccount.BankNumber = Convert.ToInt32(BankNumber.Text);
                bankAccount.BranchNumber = Convert.ToInt32(BranchNumber_ComboBox.SelectedItem);
                bankAccount.BankAccountNumber = Convert.ToInt32(BankAccountNumber.Text);

                host.BankAccount = bankAccount;

                myBL.AddHost(host);
                MessageBox.Show("Recived Seccessfully");
               
            }
            catch (Exception)
            {
                MessageBox.Show("Error! Make sure you dont miss any field!");
            }
        }

        private void Bank_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BranchNumber_ComboBox.ItemsSource = (Banks.ElementAt(Bank_ComboBox.SelectedIndex).Branches);
            BranchNumber_ComboBox.DisplayMemberPath = "BranchNumber";
            //Branches = new ObservableCollection<BankBranch>((Banks.ElementAt(Bank_ComboBox.SelectedIndex).Branches));
            BankNumber.Text = Banks.ElementAt(Bank_ComboBox.SelectedIndex).BankNumber.ToString();
        }

        private void BranchNumber_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BranchCity.Text = Banks.ElementAt(Bank_ComboBox.SelectedIndex).Branches.ElementAt(BranchNumber_ComboBox.SelectedIndex).BranchCity;
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
