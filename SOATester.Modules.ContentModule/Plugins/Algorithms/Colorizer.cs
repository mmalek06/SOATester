using SOATester.Modules.ContentModule.ViewModels;
using SOATester.Modules.ContentModule.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace SOATester.Modules.ContentModule.Plugins {
    public class Colorizer : IPlugin {

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

        public Colorizer() {
            PluginKey = PluginKey.COLORIZER;
            Strategy = Strategy.NONE;
            _occupiedColors = new HashSet<Color>();
        }

        #endregion

        #region public methods

        public IEnumerable<IPluggableViewModel> Execute(IEnumerable<IPluggableViewModel> viewModels) {
            if (viewModels != null) {
                var groups = viewModels.GroupBy(vm => vm.GetType()).OrderBy(group => group.First().Importance);
                var rand = new Random();
                var prevColorMap = new Dictionary<int, Color>();

                foreach (var group in groups) {
                    var colorMap = new Dictionary<int, Color>();

                    foreach (var vm in group) {
                        if (vm.Importance == 1) {
                            SetParentColor(vm, rand, colorMap);
                        } else {
                            SetChildColor(vm, rand, prevColorMap, colorMap);
                        }
                    }

                    prevColorMap = colorMap;
                }
            }

            return viewModels;
        }

        #endregion

        #region methods

        private void SetParentColor(IPluggableViewModel viewModel, Random rand, Dictionary<int, Color> colorMap) {
            object brush;
            bool isBrushAvailable = viewModel.ViewProperties.TryGetValue("Brush", out brush);

            if (brush == null) {
                var color = GetColor(rand);

                while (_occupiedColors.Contains(color)) {
                    color = GetColor(rand);
                }

                _occupiedColors.Add(color);
                colorMap[viewModel.Id] = color;
                viewModel.ViewProperties["Brush"] = new SolidColorBrush(color);
            } else {
                colorMap[viewModel.Id] = (viewModel.ViewProperties["Brush"] as SolidColorBrush).Color;
            }
        }

        private void SetChildColor(IPluggableViewModel viewModel, Random rand, Dictionary<int, Color> parentMap, Dictionary<int, Color> childMap) {
            Color color;
            object brush;
            bool isBrushAvailable = viewModel.ViewProperties.TryGetValue("Brush", out brush);

            if (brush == null) {
                if (!parentMap.TryGetValue(viewModel.ParentId, out color)) {
                    color = GetColor(rand);
                }

                _occupiedColors.Add(color);
                viewModel.ViewProperties["Brush"] = new SolidColorBrush(color);
            } else {
                color = (viewModel.ViewProperties["Brush"] as SolidColorBrush).Color;
            }

            childMap[viewModel.Id] = (viewModel.ViewProperties["Brush"] as SolidColorBrush).Color;
        }

        private Color GetColor(Random rand) {
            // only dark colors
            int colorNum = rand.Next(0x1000000) & 0x7F7F7F;
            byte[] bytes = BitConverter.GetBytes(colorNum);
            var color = Color.FromArgb(100, bytes[1], bytes[2], bytes[3]);

            return color;
        }

        #endregion

    }
}