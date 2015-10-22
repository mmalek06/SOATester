using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SOATester.Infrastructure.Controls {
    /// <summary>
    /// Interaction logic for SoaTesterTabControl.xaml
    /// </summary>
    public partial class SoaTesterTabControl : UserControl {

        #region static properties

        public static readonly DependencyProperty BodyProperty = DependencyProperty.Register(
            "Body",
            typeof(object),
            typeof(SoaTesterTabControl),
            new UIPropertyMetadata(null));

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
            "Items",
            typeof(ObservableCollection<object>),
            typeof(SoaTesterTabControl),
            new UIPropertyMetadata(null));

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem",
            typeof(object),
            typeof(SoaTesterTabControl),
            new UIPropertyMetadata(null));

        #endregion

        #region public properties

        public object Body {
            get { return GetValue(BodyProperty); }
            set { SetValue(BodyProperty, value); }
        }

        public ObservableCollection<object> Items {
            get { return (ObservableCollection<object>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public object SelectedItem {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public ObservableCollection<ListViewItem> ListViewItems { get; set; }

        #endregion

        public SoaTesterTabControl() {
            InitializeComponent();

            ListViewItems = new ObservableCollection<ListViewItem>();
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add) {
                var newListViewItem = new ListViewItem();
                var newDataContext = e.NewItems[0];

                newListViewItem.DataContext = newDataContext;

                ListViewItems.Add(newListViewItem);
                SelectedItem = newDataContext;
            } else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove) {
                
            }
        }

    }
}
