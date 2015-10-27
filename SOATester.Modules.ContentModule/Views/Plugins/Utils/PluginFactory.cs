using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Unity;

using Newtonsoft.Json;

using SOATester.Modules.ContentModule.Views.Plugins.Base;

namespace SOATester.Modules.ContentModule.Views.Plugins.Utils {
    public class PluginFactory {

        #region fields

        private readonly IUnityContainer _container;

        private IEnumerable<PluginConfigEntry> _configEntries;

        #endregion

        #region constructors and destructors

        public PluginFactory(IUnityContainer container) {
            _container = container;

            _initializeConfig();
        }

        #endregion

        #region public methods

        public IEnumerable<IPlugin> GetActivePlugins() {
            return GetPlugins().Where(plugin => plugin.IsActive);
        }

        public IEnumerable<IPlugin> GetPlugins() {
            var plugins = _container.Resolve<IEnumerable<IPlugin>>();

            foreach (var plugin in plugins) {
                _configurePlugin(plugin);
            }

            return plugins.OrderBy(plugin => plugin.Priority);
        }

        #endregion

        #region methods

        private void _initializeConfig() {
            using (StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"/Configs/plugins.json")) {
                var itemsData = reader.ReadToEnd();

                _configEntries = JsonConvert.DeserializeObject<IEnumerable<PluginConfigEntry>>(itemsData);
            }
        }

        private void _configurePlugin(IPlugin plugin) {
            var configEntry = _configEntries.FirstOrDefault(entry => entry.Key == plugin.PluginKey && entry.Strategy == plugin.Strategy);

            if (configEntry != null) {
                plugin.IsActive = configEntry.IsActive;
                plugin.Priority = configEntry.Priority;
            }
        }

        #endregion

        #region private classes

        private class PluginConfigEntry {
            public Enums.PluginKey Key { get; set; }
            public Enums.Strategy Strategy { get; set; }
            public bool IsActive { get; set; }
            public int Priority { get; set; }
        }

        #endregion

    }
}
