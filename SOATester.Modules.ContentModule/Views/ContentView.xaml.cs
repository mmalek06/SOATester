using Prism.Common;
using SOATester.Infrastructure.ViewModels;
using SOATester.Modules.ContentModule.Plugins;
using SOATester.Modules.ContentModule.ViewModels;
using SOATester.Modules.ContentModule.ViewModels.Base;
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

        private IEnumerable<IPlugin> Plugins;
        private bool HasAnyPlugins;

        #endregion

        #region public properties

        public ObservableCollection<IPluggableViewModel> OpenedItems { get; set; }
        public ObservableObject<IPluggableViewModel> SelectedItem { get; set; }

        #endregion

        #region constructors and destructors

        public ContentView(ContentViewModel vm, PluginFactory pluginFactory) { 
            InitializeComponent();
            
            Plugins = pluginFactory.GetActivePlugins();
            HasAnyPlugins = Plugins.Any();

            DataContext = vm;
            OpenedItems = new ObservableCollection<IPluggableViewModel>();
            SelectedItem = new ObservableObject<IPluggableViewModel>();

            vm.Items.CollectionChanged += Items_CollectionChanged;
        }
        
        #endregion

        #region event handlers

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add) {
                ItemAdded(e.NewItems[0] as IPluggableViewModel);
            } else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove) {
                ItemRemoved(e.OldItems[0] as IPluggableViewModel);
            }
        }

        private void ItemAdded(IPluggableViewModel newItem) {
            FillDefaultViewProperties(newItem);

            if (HasAnyPlugins) {
                RunPlugins(newItem);
            } else {
                OpenedItems.Add(newItem);
            }

            SelectItem(newItem);
        }

        private void ItemRemoved(IPluggableViewModel oldItem) {
            var itemToRemove = OpenedItems.First(item => item.Equals(oldItem));

            OpenedItems.Remove(itemToRemove);
        }

        #endregion

        #region methods

        private void RunPlugins(IPluggableViewModel item) {
            var viewModels = new List<IPluggableViewModel>();

            IEnumerable<IPluggableViewModel> pluginExecutionResult = null;

            viewModels.AddRange(OpenedItems);
            viewModels.Add(item);

            foreach (var plugin in Plugins) {
                pluginExecutionResult = plugin.Execute(pluginExecutionResult == null ? viewModels : pluginExecutionResult);
            }

            OpenedItems.Clear();
            OpenedItems.AddRange(pluginExecutionResult == null ? viewModels : pluginExecutionResult);
        }

        private void SelectItem(IPluggableViewModel item) {
            SelectedItem.Value = OpenedItems.FirstOrDefault(openedItem => openedItem.Equals(item));
        }

        private void FillDefaultViewProperties(IPluggableViewModel viewModel) {
            viewModel.ViewProperties["Brush"] = null;
        }

        #endregion

    }
}
