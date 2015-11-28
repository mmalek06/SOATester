using SOATester.Infrastructure;
using SOATester.Modules.ContentModule.Plugins.Base;
using SOATester.Modules.ContentModule.Plugins.Enums;
using SOATester.Modules.ContentModule.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace SOATester.Modules.ContentModule.Plugins {
    public class ColorizerPlugin : IPlugin {

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

        public ColorizerPlugin() {
            PluginKey = PluginKey.COLORIZER;
            Strategy = Strategy.FLAT;
            _occupiedColors = new HashSet<Color>();
        }

        #endregion

        #region public methods

        public IEnumerable<ViewModelBase> Execute(IEnumerable<ViewModelBase> objects) {
            var projectViewModels = objects.Where(obj => obj is ProjectViewModel);
            var projectToColorMap = new Dictionary<int, Color>();
            var scenarioViewModels = objects.Where(obj => obj is ScenarioViewModel);
            var scenarioToColorMap = new Dictionary<int, Color>();
            var testViewModels = objects.Where(obj => obj is TestViewModel);
            var testToColorMap = new Dictionary<int, Color>();
            var stepViewModels = objects.Where(obj => obj is StepViewModel);
            var rand = new Random();

            foreach (var projectViewModel in projectViewModels) {
                object brush;
                bool isBrushAvailable = projectViewModel.ViewProperties.TryGetValue("Brush", out brush);

                if (brush == null) { 
                    var color = _getColor(rand);

                    while (_occupiedColors.Contains(color)) {
                        color = _getColor(rand);
                    }

                    _occupiedColors.Add(color);
                    projectToColorMap[(projectViewModel as ProjectViewModel).Id] = color;
                    projectViewModel.ViewProperties["Brush"] = new SolidColorBrush(color);
                } else {
                    projectToColorMap[(projectViewModel as ProjectViewModel).Id] = (projectViewModel.ViewProperties["Brush"] as SolidColorBrush).Color;
                }
            }

            foreach (var proxy in scenarioViewModels) {
                _setColor(proxy, rand, projectToColorMap, scenarioToColorMap, (proxy as ScenarioViewModel).Scenario.ProjectId, (proxy as ScenarioViewModel).Id);
            }

            foreach (var proxy in testViewModels) {
                _setColor(proxy, rand, scenarioToColorMap, testToColorMap, (proxy as TestViewModel).Test.ScenarioId, (proxy as TestViewModel).Id);
            }

            foreach (var proxy in stepViewModels) {
                _setColor(proxy, rand, testToColorMap, null, (proxy as StepViewModel).Step.TestId);
            }

            return objects;
        }

        #endregion

        #region methods

        private void _setColor(ViewModelBase viewModel, Random rand, Dictionary<int, Color> parentMap, Dictionary<int, Color> childMap, int parentId, int childId = -1) {
            Color color;
            object brush;
            bool isBrushAvailable = viewModel.ViewProperties.TryGetValue("Brush", out brush);

            if (brush == null) {
                if (!parentMap.TryGetValue(parentId, out color)) {
                    color = _getColor(rand);
                }

                _occupiedColors.Add(color);
                viewModel.ViewProperties["Brush"] = new SolidColorBrush(color);
            } else {
                color = (viewModel.ViewProperties["Brush"] as SolidColorBrush).Color;
            }

            if (childId > -1) {
                childMap[childId] = (viewModel.ViewProperties["Brush"] as SolidColorBrush).Color;
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
