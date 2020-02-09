using BE;
using BL;
using System.Windows.Controls;

namespace PLWPF.Windows.Statistics
{
    /// <summary>
    /// Interaction logic for HostingUnitQueryPage.xaml
    /// </summary>
    public partial class HostingUnitQueryPage : Page
    {
        MyBl myBL = new MyBl();

        public HostingUnitQueryPage()
        {
            InitializeComponent();
             PerArea.Content = new ShowPerArea(myBL.HostingUnitPerArea());
            var v = myBL.HostingUnitPerArea();
            float sum = v[Area.Jerusalem] + v[Area.Center] + v[Area.North] + v[Area.South];
            float avr=sum / 100;

            PerAreaGraphBar.Content=new GraphBarsUserControl(
                v[Area.Jerusalem] / avr,
                v[Area.Center] / avr,
                v[Area.North] / avr,
                v[Area.South] / avr);
        }

        //

    }
}
