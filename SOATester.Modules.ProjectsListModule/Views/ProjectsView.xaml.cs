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

using SOATester.Modules.ProjectsListModule.ViewModels;

namespace SOATester.Modules.ProjectsListModule.Views {
    /// <summary>
    /// Interaction logic for ProjectsView.xaml
    /// </summary>
    public partial class ProjectsView : UserControl {
        public ProjectsView(ProjectsViewModel vm) {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
