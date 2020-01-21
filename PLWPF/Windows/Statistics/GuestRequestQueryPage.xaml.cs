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
using System.Windows.Navigation;
using System.Windows.Shapes;
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
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var v = myBL.GuestRequestPerArea();

            CenterResult.Text = v[Area.Center].ToString();
            JerusalemResult.Text = v[Area.Jerusalem].ToString();
            NorthResult.Text = v[Area.North].ToString();
            SouthResult.Text = v[Area.South].ToString();
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            var v = myBL.GuestRequestPerRquirement(Requirements.Necessary);
            Pool                  .Text=v[ "Pool"                     ].ToString()  ;
            Jacuzzi               .Text=v[ "Jacuzzi"                  ].ToString()  ;
            Garden                .Text=v[ "Garden"                   ].ToString()  ;
            ChildrensAttractions  .Text=v[ "ChildrensAttractions"     ].ToString()  ;
            SpredBads             .Text=v[ "SpredBads"                ].ToString()  ;
            AirCondsner           .Text=v[ "AirCondsner"              ].ToString()  ;
            frisider              .Text=v[ "frisider"                 ].ToString()  ;
            SingogNaerBy          .Text=v[ "SingogNaerBy"             ].ToString()  ;
            NaerPublicTrensportion.Text = v["NaerPublicTrensportion"].ToString();
        }
    }
}
