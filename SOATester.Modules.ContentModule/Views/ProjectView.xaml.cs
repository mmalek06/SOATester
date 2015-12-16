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

using SOATester.Modules.ContentModule.ViewModels;

namespace SOATester.Modules.ContentModule.Views {
    /// <summary>
    /// Interaction logic for ProjectView.xaml
    /// </summary>
    public partial class ProjectView : UserControl {
        public ProjectView(ProjectViewModel vm) {
            InitializeComponent();

            DataContext = vm;
        }
    }
}
