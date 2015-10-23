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
                Color color;

                if (proxy.Brush == null) {
                    if (!scenarioToColorMap.TryGetValue((proxy.ViewModel as ScenarioViewModel).Scenario.ProjectId, out color)) {
                        color = _getColor(rand);
                    }

                    _occupiedColors.Add(color);
                    proxy.Brush = new SolidColorBrush(color);
                } else {
                    color = proxy.Brush.Color;
                }

                scenarioToColorMap[(proxy.ViewModel as ScenarioViewModel).Id] = proxy.Brush.Color;
            }

            foreach (var proxy in testsProxies) {
                Color color;

                if (proxy.Brush == null) {
                    if (!projectToColorMap.TryGetValue((proxy.ViewModel as TestViewModel).Test.ScenarioId, out color)) {
                        color = _getColor(rand);
                    }

                    _occupiedColors.Add(color);
                    proxy.Brush = new SolidColorBrush(color);
                } else {
                    color = proxy.Brush.Color;
                }

                testToColorMap[(proxy.ViewModel as TestViewModel).Id] = proxy.Brush.Color;
            }

            foreach (var proxy in stepsProxies) {
                if (proxy.Brush == null) {
                    Color color;

                    if (!testToColorMap.TryGetValue((proxy.ViewModel as StepViewModel).Step.TestSuiteId, out color)) {
                        color = _getColor(rand);
                    }

                    _occupiedColors.Add(color);
                    proxy.Brush = new SolidColorBrush(color);
                }
            }

            return objects;
        }

        #endregion

        #region private methods

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
