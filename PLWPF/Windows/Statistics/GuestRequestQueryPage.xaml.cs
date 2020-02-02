using BL;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BE;
namespace PLWPF.Windows.Statistics
{
    /// <summary>
    /// Interaction logic for GuestRequestQueryPage.xaml
    /// </summary>
    public partial class GuestRequestQueryPage : Page
    {
        MyBl myBL = new MyBl();

        public GuestRequestQueryPage()
        {
            InitializeComponent();
            LevelOfDemand_ComboBox.ItemsSource = Enum.GetValues(typeof(BE.Requirements)).Cast<BE.Requirements>();
            PerArea.Content = new ShowPerArea(myBL.GuestRequestPerArea());
        }


        private void LevelOfDemand_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var v = myBL.GuestRequestPerRquirement((Requirements)LevelOfDemand_ComboBox.SelectedItem);
            Pool.Text = v["Pool"].ToString();
            Jacuzzi.Text = v["Jacuzzi"].ToString();
            Garden.Text = v["Garden"].ToString();
            ChildrensAttractions.Text = v["ChildrensAttractions"].ToString();
            SpredBads.Text = v["SpredBads"].ToString();
            AirCondsner.Text = v["AirCondsner"].ToString();
            frisider.Text = v["frisider"].ToString();
            SingogNaerBy.Text = v["SingogNaerBy"].ToString();
            NaerPublicTrensportion.Text = v["NaerPublicTrensportion"].ToString();
        
        }


    }
}

                   
