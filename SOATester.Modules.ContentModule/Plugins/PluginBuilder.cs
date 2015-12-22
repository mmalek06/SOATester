using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SOATester.Modules.ContentModule.Plugins {
    public class PluginBuilder {

        #region fields

        private readonly IUnityContainer container;
        private IEnumerable<PluginConfigEntry> configEntries;

        #endregion

        #region constructors and destructors

        public PluginBuilder(IUnityContainer container) {
            this.container = container;

            InitializeConfig();
        }

        #endregion

        #region public methods

        public IEnumerable<PluginBase> GetActivePlugins() {
            return GetPlugins().Where(plugin => plugin.IsActive);
        }

        public IEnumerable<PluginBase> GetPlugins() {
            var plugins = container.Resolve<IEnumerable<PluginBase>>();

            foreach (var plugin in plugins) {
                ConfigurePlugin(plugin);
            }

            return plugins.OrderBy(plugin => plugin.Priority);
        }

        #endregion

        #region methods

        private void InitializeConfig() {
            using (StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"/Plugins/Configs/plugins.json")) {
                var itemsData = reader.ReadToEnd();

                configEntries = JsonConvert.DeserializeObject<IEnumerable<PluginConfigEntry>>(itemsData);
            }
        }

        private void ConfigurePlugin(PluginBase plugin) {
            var configEntry = configEntries.FirstOrDefault(entry => entry.Key == plugin.PluginKey && entry.Strategy == plugin.Strategy);

            if (configEntry != null) {
                plugin.IsActive = configEntry.IsActive;
                plugin.Priority = configEntry.Priority;
            } else {
                throw new ArgumentException("No config entry for plugin");
            }
        }

        #endregion

        #region private classes

        private class PluginConfigEntry {
            public PluginKey Key { get; set; }
            public Strategy Strategy { get; set; }
            public bool IsActive { get; set; }
            public int Priority { get; set; }
        }

        #endregion

    }
}
