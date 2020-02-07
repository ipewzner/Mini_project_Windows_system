using BL;
using System.Linq;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows;

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
            GuestRequestListView.ItemsSource = myBL.GuestRequestBy();
            view = (CollectionView)CollectionViewSource.GetDefaultView(GuestRequestListView.ItemsSource);
        }

        private void GuestRequestListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        
        //-----------------------------------------------------------------------------------------------

        GridViewColumnHeader _lastHeaderClicked = null;

        private void ListViewColumnHeaderClick(object sender, System.Windows.RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;

            if (headerClicked == null)
                return;

            if (headerClicked.Role == GridViewColumnHeaderRole.Padding)
                return;

            var sortingColumn = (headerClicked.Column.DisplayMemberBinding as Binding)?.Path?.Path;
            if (sortingColumn == null)
                return;

            var direction = ApplySort(view, sortingColumn);
                  

            if (direction == ListSortDirection.Ascending)
            {
                headerClicked.Column.HeaderTemplate =
                    Resources["HeaderTemplateArrowUp"] as DataTemplate;
            }
            else
            {
                headerClicked.Column.HeaderTemplate =
                    Resources["HeaderTemplateArrowDown"] as DataTemplate;
            }

            // Remove arrow from previously sorted header
            if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
            {
                _lastHeaderClicked.Column.HeaderTemplate =
                    Resources["HeaderTemplateDefault"] as DataTemplate;
            }

            _lastHeaderClicked = headerClicked;
        }

        public static ListSortDirection ApplySort(ICollectionView view, string propertyName)
        {
            ListSortDirection direction = ListSortDirection.Ascending;
            if (view.SortDescriptions.Count > 0)
            {
                SortDescription currentSort = view.SortDescriptions[0];
                if (currentSort.PropertyName == propertyName)
                {
                    if (currentSort.Direction == ListSortDirection.Ascending)
                        direction = ListSortDirection.Descending;
                    else
                        direction = ListSortDirection.Ascending;
                }
                view.SortDescriptions.Clear();
            }
            if (!string.IsNullOrEmpty(propertyName))
            {
                view.SortDescriptions.Add(new SortDescription(propertyName, direction));
            }
            return direction;
        }

    }
}


