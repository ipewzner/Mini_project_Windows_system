using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace PLWPF.Windows
{
    /// <summary>
    /// Interaction logic for HostingUnitPage.xaml
    /// </summary>
    public partial class HostingUnitPage : Page
    {
        public HostingUnitPage(HostingUnit hostingUnit)
        {

            InitializeComponent();
            UnitArea.Text = hostingUnit.UnitArea.ToString();
            //hostingUnit.Diary;
            Clendar.DisplayDateStart = new DateTime(2020, 1, 2);
            Clendar.DisplayDateEnd = new DateTime(2040, 1, 5);
         }
    }  
}
