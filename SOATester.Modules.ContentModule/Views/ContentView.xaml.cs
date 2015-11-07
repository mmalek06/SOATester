using Prism.Common;
using SOATester.Infrastructure;
using SOATester.Modules.ContentModule.Plugins;
using SOATester.Modules.ContentModule.Plugins.Base;
using SOATester.Modules.ContentModule.ViewModels;
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

namespace SOATester.Modules.ContentModule.Views {
    public partial class ContentView : UserControl {

        #region fields

        private IEnumerable<IPlugin> _plugins;
        private bool _hasAnyPlugins;

        #endregion

        #region public properties

        public ObservableCollection<ViewModelBase> OpenedItems { get; set; }
        public ObservableObject<ViewModelBase> SelectedItem { get; set; }

        #endregion

        #region constructors and destructors

        //public ContentView(ContentViewModel vm, IEnumerable<IPlugin> plugins) {
        public ContentView(ContentViewModel vm, PluginFactory pluginFactory) { 
            InitializeComponent();
            
            _plugins = pluginFactory.GetActivePlugins();
            _hasAnyPlugins = _plugins.Any();

            DataContext = vm;
            OpenedItems = new ObservableCollection<ViewModelBase>();
            SelectedItem = new ObservableObject<ViewModelBase>();

            vm.Items.CollectionChanged += Items_CollectionChanged;
        }
        
        #endregion

        #region event handlers

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add) {
                _itemAdded(e.NewItems[0] as ViewModelBase);
            } else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove) {
                _itemRemoved(e.OldItems[0] as ViewModelBase);
            }
        }

        private void _itemAdded(ViewModelBase newItem) {
            _fillDefaultViewProperties(newItem);

            if (_hasAnyPlugins) {
                _runPlugins(newItem);
            } else {
                OpenedItems.Add(newItem);
            }

            _selectItem(newItem);
        }

        private void _itemRemoved(ViewModelBase oldItem) {
            var itemToRemove = OpenedItems.First(item => item.Equals(oldItem));

            OpenedItems.Remove(itemToRemove);
        }

        #endregion

        #region methods

        private void _runPlugins(ViewModelBase item) {
            var viewModels = new List<ViewModelBase>();

            IEnumerable<ViewModelBase> pluginExecutionResult = null;

            viewModels.AddRange(OpenedItems);
            viewModels.Add(item);

            foreach (var plugin in _plugins) {
                pluginExecutionResult = plugin.Execute(pluginExecutionResult == null ? viewModels : pluginExecutionResult);
            }

            OpenedItems.Clear();
            OpenedItems.AddRange(pluginExecutionResult == null ? viewModels : pluginExecutionResult);
        }

        private void _selectItem(ViewModelBase item) {
            SelectedItem.Value = OpenedItems.FirstOrDefault(openedItem => openedItem.Equals(item));
        }

        private void _fillDefaultViewProperties(ViewModelBase viewModel) {
            viewModel.ViewProperties["Brush"] = null;
        }

        #endregion

    }
}
