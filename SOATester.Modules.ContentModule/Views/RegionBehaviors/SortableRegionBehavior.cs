using Prism.Regions;
using SOATester.Modules.ContentModule.ViewModels;
using System.Windows;

namespace SOATester.Modules.ContentModule.Views.RegionBehaviors {
    internal class SortableRegionBehavior : RegionBehavior {

        #region methods

        protected override void OnAttach() {
            Region.SortComparison = CompareViews;
        }

        private int CompareViews(object x, object y) {
            if (x.Equals(y)) {
                return 0;
            }

            int order1 = (int)(((PluggableViewModel)((FrameworkElement)x).DataContext).PluggableProperties["Order"]);
            int order2 = (int)(((PluggableViewModel)((FrameworkElement)y).DataContext).PluggableProperties["Order"]);

            return order1.CompareTo(order2);
        }

        #endregion

    }
}
