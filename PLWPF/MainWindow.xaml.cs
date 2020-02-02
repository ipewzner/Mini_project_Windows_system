﻿using System;
using System.Windows;
using BE;
using BL;
using PLWPF.Windows;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Host host = null;
        MyBl myBL = new MyBl();

        public MainWindow()
        {

            InitializeComponent();
            //preCode preCode = new preCode();
            //preCode.initialize();
        }

        private void AddRequest_Click(object sender, RoutedEventArgs e)
        {
            //Task t = new Task(() => { });
            Window win = new AddRequestWindow();
                win.Show();

        }

        private void HostingUnit_Click(object sender, RoutedEventArgs e)
        {
            if (host != null)
            {
                Window win = new HostingUnitWindow(host);
            win.Show();
                Window win2 = new HostUpdateWindow(host);
                win2.Show();

            }
            else MessageBox.Show("Select User Or Sign Up");
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            if (host != null)
            {
                Window win = new OrdersWindow(host);
                win.Show();
            }
            else MessageBox.Show("Select User Or Sign Up");
        }

        private void Stat_Click(object sender, RoutedEventArgs e)
        {
            Window win = new StatisticsWindow();
            win.Show();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogInWindow win = new LogInWindow();
                win.ShowDialog();
                if (win.DialogResult == true)
                {
                    host = win.HostfromLogin;
                    user.Content = host.PrivateName + " " + host.FamilyName;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Select User Or Sign Up");

            }

        }
    }
}
