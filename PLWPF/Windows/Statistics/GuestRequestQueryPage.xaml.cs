using BL;
using System.Linq;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;

namespace PLWPF.Windows.Statistics
{
    /// <summary>
    /// Interaction logic for GuestRequestQueryPage.xaml
    /// </summary>
    public partial class GuestRequestQueryPage : Page
    {
        MyBl myBL = new MyBl();
        CollectionView view;
        public GuestRequestQueryPage()
        {
            InitializeComponent();

            //**********************************************************

            GuestRequestListView.ItemsSource = myBL.GuestRequestOrderBy_Location().SelectMany(x=>x);
             view = (CollectionView)CollectionViewSource.GetDefaultView(GuestRequestListView.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Area");
            view.GroupDescriptions.Add(groupDescription);

        }

        private void GuestRequestListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            view.SortDescriptions.Add(new SortDescription("EntryDate", ListSortDirection.Ascending));
        }
    }
    
}


/*


 Pool"                   
 Jacuzzi"                
 Garden"                 
 ChildrensAttractions"   
 SpredBads"              
 AirCondsner"            
 frisider"               
 SingogNaerBy"           
 NaerPublicTrensportion"        
*/
