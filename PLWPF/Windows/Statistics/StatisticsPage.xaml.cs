﻿using System;
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
    /// Interaction logic for StatisticsPage.xaml
    /// </summary>
    public partial class StatisticsPage : Page
    {
        public StatisticsPage()
        {
            InitializeComponent();
        }

        private void GuestRequestquery_Click(object sender, RoutedEventArgs e)
        {
           // StatisticstFrame.Content = new GuestRequestQueryPage();

           // NavigationService.Navigate(GuestRequestQueryPage());

        }

        private void HustingUnitQuery_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OrdersQuery_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExtraQuery_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
