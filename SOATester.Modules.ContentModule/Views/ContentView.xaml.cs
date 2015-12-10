using SOATester.Modules.ContentModule.Plugins;
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

        #region constructors and destructors

        public ContentView(ContentViewModel vm, PluginFactory pluginFactory) { 
            InitializeComponent();

            DataContext = vm;
        }

        #endregion

    }
}
