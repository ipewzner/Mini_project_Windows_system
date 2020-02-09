using System.Windows;
using System.Windows.Controls;

namespace PLWPF.Windows.Statistics
{
    /// <summary>
    /// Interaction logic for GraphBarsUserControl.xaml
    /// </summary>
    public partial class GraphBarsUserControl : UserControl
    {
        public GraphBarsUserControl(double first, double second, double third=0, double fourth=0, double fifth=0)
        {
            
          InitializeComponent();
            Column0.Width = new GridLength(first);
            Column1.Width = new GridLength(second);
            Column2.Width = new GridLength(third);
            Column3.Width = new GridLength(fourth);
            Column4.Width = new GridLength(fifth);
            Labe1.Content = first  + "%";
            Labe2.Content = second + "%";
            Labe3.Content = third  + "%";
            Labe4.Content = fourth + "%";
            Labe5.Content = fifth  + "%";
        }
    }
}
