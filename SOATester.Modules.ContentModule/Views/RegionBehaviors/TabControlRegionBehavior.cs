using Prism.Regions;
using SOATester.Modules.ContentModule.Plugins;
using SOATester.Modules.ContentModule.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace SOATester.Modules.ContentModule.Views.RegionBehaviors {
    public class TabControlRegionBehavior : RegionBehavior {

        #region fields

        private IEnumerable<IPlugin> plugins;
        private PluginFactory pluginFactory;
        private bool hasAnyPlugins;

        #endregion

        #region constructor

        public TabControlRegionBehavior(PluginFactory pluginFactory) : base() {
            this.pluginFactory = pluginFactory;
            plugins = pluginFactory.GetActivePlugins();
            hasAnyPlugins = plugins.Any();
        }

        private void Views_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove) {
                if (!(e.OldItems[0] is UserControl)) {
                    return;
                }

                var control = (e.OldItems[0] as UserControl);
                var vm = control.DataContext as PluggableViewModel;

                foreach (var plugin in plugins) {
                    plugin.CleanupState(vm);
                }
            }
        }

        #endregion

        #region methods

        protected override void OnAttach() {
            (Region.Views as ViewsCollection).SortComparison = CompareVms;

            Region.Views.CollectionChanged += Views_CollectionChanged;
        }

        private int CompareVms(object first, object second) {
            var preparationResult = PrepareVms(first, second);

            if (!preparationResult.SuccessfullyPrepared) {
                return 0;
            }

            RunPlugins(preparationResult.ViewModels);

            int firstOrder = (int)preparationResult.FirstVm.PluggableProperties["Order"];
            int secondOrder = (int)preparationResult.SecondVm.PluggableProperties["Order"];

            return firstOrder.CompareTo(secondOrder);
        }

        private SortPreparationResult PrepareVms(object first, object second) {
            if (!(first is UserControl) && !(second is UserControl)) {
                return new SortPreparationResult { SuccessfullyPrepared = false };
            }

            var firstView = (UserControl)first;
            var secondView = (UserControl)second;

            if (!(firstView.DataContext is PluggableViewModel) && !(secondView.DataContext is PluggableViewModel)) {
                return new SortPreparationResult { SuccessfullyPrepared = false };
            }

            var firstVm = (PluggableViewModel)firstView.DataContext;
            var secondVm = (PluggableViewModel)secondView.DataContext;

            if (firstVm.Identity == null || secondVm.Identity == null) {
                return new SortPreparationResult { SuccessfullyPrepared = false };
            }

            var alreadyAddedVms = new HashSet<PluggableViewModel>(Region.Views.Select(obj => (obj as UserControl).DataContext as PluggableViewModel));

            alreadyAddedVms.Add(firstVm);
            alreadyAddedVms.Add(secondVm);

            return new SortPreparationResult {
                SuccessfullyPrepared = true,
                FirstVm = firstVm,
                SecondVm = secondVm,
                ViewModels = alreadyAddedVms
            };
        }

        private void RunPlugins(IEnumerable<PluggableViewModel> viewModels) {
            IEnumerable<PluggableViewModel> pluginExecutionResult = null;

            foreach (var plugin in plugins) {
                if (!plugin.CheckShouldExecute(viewModels)) {
                    continue;
                }

                pluginExecutionResult = plugin.Execute(pluginExecutionResult == null ? viewModels : pluginExecutionResult);
            }
        }

        private struct SortPreparationResult {
            public bool SuccessfullyPrepared { get; set; }

            public PluggableViewModel FirstVm { get; set; }

            public PluggableViewModel SecondVm { get; set; }

            public IEnumerable<PluggableViewModel> ViewModels { get; set; }
        }

        #endregion

    }
}
