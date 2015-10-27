using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

using SOATester.Modules.ContentModule.ViewModels;
using SOATester.Modules.ContentModule.Views.Plugins.Base;
using SOATester.Modules.ContentModule.Views.Plugins.Utils;
using SOATester.Modules.ContentModule.Views.Plugins.Utils.Enums;

namespace SOATester.Modules.ContentModule.Views.Plugins.Classes {
    public class TabColorizer : IPlugin {

        #region fields

        private HashSet<Color> _occupiedColors;

        #endregion

        #region public properties

        public PluginKey PluginKey { get; set; }

        public Strategy Strategy { get; set; }

        public bool IsActive { get; set; }

        public int Priority { get; set; }

        #endregion

        #region constructors and destructors

        public TabColorizer() {
            PluginKey = PluginKey.COLORIZER;
            Strategy = Strategy.FLAT;
            _occupiedColors = new HashSet<Color>();
        }

        #endregion

        #region public methods

        public IEnumerable<TabItemProxy> Execute(IEnumerable<TabItemProxy> objects) {
            var projectsProxies = objects.Where(obj => obj.ViewModel is ProjectViewModel);
            var projectToColorMap = new Dictionary<int, Color>();
            var scenariosProxies = objects.Where(obj => obj.ViewModel is ScenarioViewModel);
            var scenarioToColorMap = new Dictionary<int, Color>();
            var testsProxies = objects.Where(obj => obj.ViewModel is TestViewModel);
            var testToColorMap = new Dictionary<int, Color>();
            var stepsProxies = objects.Where(obj => obj.ViewModel is StepViewModel);
            var rand = new Random();

            foreach (var proxy in projectsProxies) {
                if (proxy.Brush == null) {
                    var color = _getColor(rand);

                    while (_occupiedColors.Contains(color)) {
                        color = _getColor(rand);
                    }

                    _occupiedColors.Add(color);
                    projectToColorMap[(proxy.ViewModel as ProjectViewModel).Id] = color;
                    proxy.Brush = new SolidColorBrush(color);
                } else {
                    projectToColorMap[(proxy.ViewModel as ProjectViewModel).Id] = proxy.Brush.Color;
                }
            }

            foreach (var proxy in scenariosProxies) {
                _setColor(proxy, rand, projectToColorMap, scenarioToColorMap, (proxy.ViewModel as ScenarioViewModel).Scenario.ProjectId, (proxy.ViewModel as ScenarioViewModel).Id);
            }

            foreach (var proxy in testsProxies) {
                _setColor(proxy, rand, scenarioToColorMap, testToColorMap, (proxy.ViewModel as TestViewModel).Test.ScenarioId, (proxy.ViewModel as TestViewModel).Id);
            }

            foreach (var proxy in stepsProxies) {
                _setColor(proxy, rand, testToColorMap, null, (proxy.ViewModel as StepViewModel).Step.TestId);
            }

            return objects;
        }

        #endregion

        #region methods

        private void _setColor(TabItemProxy proxy, Random rand, Dictionary<int, Color> parentMap, Dictionary<int, Color> childMap, int parentId, int childId = -1) {
            Color color;

            if (proxy.Brush == null) {
                if (!parentMap.TryGetValue(parentId, out color)) {
                    color = _getColor(rand);
                }

                _occupiedColors.Add(color);
                proxy.Brush = new SolidColorBrush(color);
            } else {
                color = proxy.Brush.Color;
            }

            if (childId > -1) {
                childMap[childId] = proxy.Brush.Color;
            }
        }

        private Color _getColor(Random rand) {
            // only dark colors
            int colorNum = rand.Next(0x1000000) & 0x7F7F7F;
            byte[] bytes = BitConverter.GetBytes(colorNum);
            var color = Color.FromArgb(100, bytes[1], bytes[2], bytes[3]);

            return color;
        }

        #endregion

    }
}
