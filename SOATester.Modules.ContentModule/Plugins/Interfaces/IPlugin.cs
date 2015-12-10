using SOATester.Modules.ContentModule.ViewModels;
using System.Collections.Generic;

namespace SOATester.Modules.ContentModule.Plugins {
    public interface IPlugin {

        #region properties

        bool IsActive { get; set; }
        int Priority { get; set; }
        PluginKey PluginKey { get; set; }
        Strategy Strategy { get; set; }

        #endregion

        #region methods

        IEnumerable<IPluggableViewModel> Execute(IEnumerable<IPluggableViewModel> parameter);

        #endregion

    }
}
