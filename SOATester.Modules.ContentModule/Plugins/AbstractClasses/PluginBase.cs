using System.Collections.Generic;
using SOATester.Modules.ContentModule.ViewModels;

namespace SOATester.Modules.ContentModule.Plugins {
    public abstract class PluginBase {

        #region properties

        public PluginKey PluginKey { get; set; }
        public Strategy Strategy { get; set; }
        public bool IsActive { get; set; }
        public int Priority { get; set; }

        #endregion

        #region public methods

        public abstract IEnumerable<PluggableViewModel> Execute(IEnumerable<PluggableViewModel> viewModels);

        #endregion

    }
}
