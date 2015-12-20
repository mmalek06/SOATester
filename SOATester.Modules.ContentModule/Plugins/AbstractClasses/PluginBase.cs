using System.Collections.Generic;
using System.Linq;
using SOATester.Modules.ContentModule.ViewModels;

namespace SOATester.Modules.ContentModule.Plugins {
    public abstract class PluginBase : IPlugin {

        #region fields

        private List<int> lastRunResult;

        #endregion

        #region properties

        public PluginKey PluginKey { get; set; }
        public Strategy Strategy { get; set; }
        public bool IsActive { get; set; }
        public int Priority { get; set; }

        #endregion

        #region constructor

        public PluginBase() {
            lastRunResult = new List<int>();
        }

        #endregion

        #region public methods

        public bool CheckShouldExecute(IEnumerable<PluggableViewModel> viewModels) {
            if (viewModels == null) {
                return false;
            }

            int vmCount = viewModels.Count();

            if (vmCount < lastRunResult.Count || (vmCount == lastRunResult.Count && viewModels.OrderBy(vm => vm.GetHashCode()).Select(vm => vm.GetHashCode()).SequenceEqual(lastRunResult))) {
                return false;
            }

            return true;
        }

        public abstract IEnumerable<PluggableViewModel> Execute(IEnumerable<PluggableViewModel> viewModels);

        #endregion

        #region methods

        protected void SetLastRunResult(IEnumerable<PluggableViewModel> result) {
            var localResult = result as PluggableViewModel[];

            lastRunResult.Clear();

            for (int i = 0; i < localResult.Length; i++) {
                localResult[i].PluggableProperties["Order"] = i;
                lastRunResult.Add(localResult[i].GetHashCode());
            }

            lastRunResult = lastRunResult.OrderBy(num => num).ToList();
        }

        #endregion

    }
}
