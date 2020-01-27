using System.Collections.Generic;
using System.Windows.Controls;
using BE;

namespace PLWPF.Windows.Statistics
{
    /// <summary>
    /// Interaction logic for ShowPerArea.xaml
    /// </summary>
    public partial class ShowPerArea : UserControl
    {
        public ShowPerArea(Dictionary<Area, int> func)
        {
            InitializeComponent();
            var v = func;
            CenterResult.Text = v[Area.Center].ToString();
            JerusalemResult.Text = v[Area.Jerusalem].ToString();
            NorthResult.Text = v[Area.North].ToString();
            SouthResult.Text = v[Area.South].ToString();

        }
    }
}
