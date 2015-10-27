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
using System.Collections.ObjectModel;

using Prism.Common;

using SOATester.Modules.ContentModule.ViewModels;
using SOATester.Modules.ContentModule.ViewModels.Base;
using SOATester.Modules.ContentModule.Views.Plugins;
using SOATester.Modules.ContentModule.Views.Plugins.Utils;
using SOATester.Modules.ContentModule.Views.Plugins.Base;

namespace SOATester.Modules.ContentModule.Views {
    public partial class ContentView : UserControl {

        #region fields

        private IEnumerable<IPlugin> _plugins;
        private VmWrapper _vmWrapper;

        #endregion

        #region public properties

        public ObservableCollection<TabItemProxy> OpenedItems { get; set; }
        public ObservableObject<TabItemProxy> SelectedItem {
            get;
            set; }

        #endregion

        #region constructors and destructors

        //public ContentView(ContentViewModel vm, IEnumerable<IPlugin> plugins) {
        public ContentView(ContentViewModel vm, PluginFactory pluginFactory) { 
            InitializeComponent();
            
            _plugins = pluginFactory.GetActivePlugins();
            _vmWrapper = new VmWrapper();

            DataContext = vm;
            OpenedItems = new ObservableCollection<TabItemProxy>();
            SelectedItem = new ObservableObject<TabItemProxy>();

            vm.Items.CollectionChanged += Items_CollectionChanged;
        }
        
        #endregion

        #region event handlers

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add) {
                _itemAdded(e.NewItems[0] as IViewModel);
            } else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove) {
                _itemRemoved(e.OldItems[0] as IViewModel);
            }
        }

        private void _itemAdded(IViewModel newItem) {
            var wrappedItem = _vmWrapper.WrapObject(newItem);

            _runPlugins(wrappedItem);
            _selectItem(wrappedItem);
        }

        private void _itemRemoved(IViewModel oldItem) {
            var itemToRemove = OpenedItems.First(item => item.ViewModel.Equals(oldItem));

            OpenedItems.Remove(itemToRemove);
        }

        #endregion

        #region methods

        private void _runPlugins(TabItemProxy item) {
            var proxies = new List<TabItemProxy>();
            IEnumerable<TabItemProxy> pluginExecutionResult = null;

            proxies.AddRange(OpenedItems);
            proxies.Add(item);

            foreach (var plugin in _plugins) {
                pluginExecutionResult = plugin.Execute(pluginExecutionResult == null ? proxies : pluginExecutionResult);
            }

            OpenedItems.Clear();
            OpenedItems.AddRange(pluginExecutionResult == null ? proxies : pluginExecutionResult);
        }

        private void _selectItem(TabItemProxy item) {
            SelectedItem.Value = OpenedItems.FirstOrDefault(openedItem => openedItem.ViewModel.Equals(item.ViewModel));
        }

        #endregion

    }
}
