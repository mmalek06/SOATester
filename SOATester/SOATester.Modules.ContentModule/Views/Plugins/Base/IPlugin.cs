using System.Collections.Generic;

using SOATester.Modules.ContentModule.ViewModels.Base;
using SOATester.Modules.ContentModule.Views.Plugins.Utils;
using SOATester.Modules.ContentModule.Views.Plugins.Utils.Enums;

namespace SOATester.Modules.ContentModule.Views.Plugins.Base {
    public interface IPlugin : SOATester.Infrastructure.Plugins.IPlugin {

        #region properties

        PluginKey PluginKey { get; set; }
        Strategy Strategy { get; set; }

        #endregion

        #region methods

        IEnumerable<TabItemProxy> Execute(IEnumerable<TabItemProxy> parameter);

        #endregion

    }
}
