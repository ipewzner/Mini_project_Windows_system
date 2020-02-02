using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

    public partial class Register : INotifyPropertyChanged
    {
        MyBl myBL = new MyBl();
        public IEnumerable<BankDetails> Banks { get; set; }
        public string selectedBank { get; set; }

        public ObservableCollection<BankBranch> Branches { get; set; }

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


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Register()
        {
            InitializeComponent();
            DataContext = this;
            Banks = myBL.GetBanks();
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
                bankAccount.BranchCity = BranchCity.Text;
                //bankAccount.BranchAddress = Convert.ToInt32(.Text);
                bankAccount.BankNumber = Convert.ToInt32(BankNumber.Text);
                bankAccount.BranchNumber = Convert.ToInt32(BranchNumber_ComboBox.SelectedItem);
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
    }
}

            //Host_UserControl.Content = new HostUserControl(null,false);
 
