using SOATester.Infrastructure;
using SOATester.Modules.ContentModule.Plugins.Enums;
using System.Collections.Generic;

namespace SOATester.Modules.ContentModule.Plugins.Base {
    public interface IPlugin : Infrastructure.Plugins.IPlugin {

        #region properties

        PluginKey PluginKey { get; set; }
        Strategy Strategy { get; set; }

        #endregion

        #region methods

        IEnumerable<ViewModelBase> Execute(IEnumerable<ViewModelBase> parameter);

        #endregion

    }
}
