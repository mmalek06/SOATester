using SOATester.Infrastructure;
using SOATester.Modules.ContentModule.Plugins.Base;
using SOATester.Modules.ContentModule.Plugins.Enums;
using SOATester.Modules.ContentModule.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace SOATester.Modules.ContentModule.Plugins {
    public class AggregatorPlugin : IPlugin {
        
        #region public properties

        public PluginKey PluginKey { get; set; }
        public Strategy Strategy { get; set; }
        public bool IsActive { get; set; }
        public int Priority { get; set; }

        #endregion

        #region constructors and destructors

        public AggregatorPlugin() {
            PluginKey = PluginKey.AGGREGATOR;
            Strategy = Strategy.NONE;
        }

        #endregion

        #region public methods

        public IEnumerable<ViewModelBase> Execute(IEnumerable<ViewModelBase> objects) {
            if (objects != null) {
                var projectsMatchingResult = _matchProjectsWithKeys(objects);
                var scenariosMatchingResult = _matchScenariosWithKeys(objects, projectsMatchingResult.Lookup);
                var testsMatchingResult = _matchTestsWithKeys(objects, scenariosMatchingResult.Lookup);
                var stepsMatchingResult = _matchStepsWithKeys(objects, testsMatchingResult.Lookup);
                var dictionaries = new List<Dictionary<int, ViewModelBase>>();

                dictionaries.Add(projectsMatchingResult.Mapping);
                dictionaries.Add(scenariosMatchingResult.Mapping);
                dictionaries.Add(testsMatchingResult.Mapping);
                dictionaries.Add(stepsMatchingResult);

                var flatDictionary = dictionaries.SelectMany(dict => dict).ToDictionary(pair => pair.Key, pair => pair.Value);
                var orderedEnumerable = flatDictionary.OrderBy(pair => pair.Key);
                var result = orderedEnumerable.Select(entry => entry.Value);
                
                return result;
            }

            return null;
        }

        #endregion

        #region methods

        private MatchingResult _matchProjectsWithKeys(IEnumerable<ViewModelBase> objects) {
            var projectsProxiesWithKeys = new Dictionary<int, ViewModelBase>();
            var projectsLookup = new Dictionary<int, IndexedViewModel>();
            const int StepSize = 100000;
            int step = StepSize;

            foreach (var obj in objects.Where(obj => obj is ProjectViewModel)) {
                projectsProxiesWithKeys[step] = obj;
                projectsLookup[(obj as ProjectViewModel).Id] = new IndexedViewModel {
                    Index = step,
                    ViewModel = obj
                };

                step += StepSize;
            }

            return new MatchingResult {
                Mapping = projectsProxiesWithKeys,
                Lookup = projectsLookup
            };
        }

        private MatchingResult _matchScenariosWithKeys(IEnumerable<ViewModelBase> objects, Dictionary<int, IndexedViewModel> projectsLookup) {
            var scenariosProxiesWithKeys = new Dictionary<int, ViewModelBase>();
            var scenariosLookup = new Dictionary<int, IndexedViewModel>();
            var counts = new Dictionary<int, int>();
            int maxUnboundIdx = 100000;
            const int StepSize = 10000;

            foreach (var obj in objects.Where(obj => obj is ScenarioViewModel)) {
                int id = (obj as ScenarioViewModel).Scenario.ProjectId;
                int step = _getStep(ref maxUnboundIdx, StepSize, id, counts, projectsLookup);

                scenariosProxiesWithKeys[step] = obj;
                scenariosLookup[(obj as ScenarioViewModel).Id] = new IndexedViewModel {
                    Index = step,
                    ViewModel = obj
                };
            }

            return new MatchingResult {
                Mapping = scenariosProxiesWithKeys,
                Lookup = scenariosLookup
            };
        }

        private MatchingResult _matchTestsWithKeys(IEnumerable<ViewModelBase> objects, Dictionary<int, IndexedViewModel> scenariosLookup) {
            var testsProxiesWithKeys = new Dictionary<int, ViewModelBase>();
            var testsLookup = new Dictionary<int, IndexedViewModel>();
            var counts = new Dictionary<int, int>();
            int maxUnboundIdx = 10000;
            const int StepSize = 1000;

            foreach (var obj in objects.Where(obj => obj is TestViewModel)) {
                int id = (obj as TestViewModel).Test.ScenarioId;
                int step = _getStep(ref maxUnboundIdx, StepSize, id, counts, scenariosLookup);

                testsProxiesWithKeys[step] = obj;
                testsLookup[(obj as TestViewModel).Id] = new IndexedViewModel {
                    Index = step,
                    ViewModel = obj
                };
            }

            return new MatchingResult {
                Mapping = testsProxiesWithKeys,
                Lookup = testsLookup
            };
        }

        private Dictionary<int, ViewModelBase> _matchStepsWithKeys(IEnumerable<ViewModelBase> objects, Dictionary<int, IndexedViewModel> testsLookup) {
            var stepsProxiesWithKeys = new Dictionary<int, ViewModelBase>();
            var counts = new Dictionary<int, int>();
            int maxUnboundIdx = 1000;
            const int StepSize = 100;

            foreach (var obj in objects.Where(obj => obj is StepViewModel)) {
                int id = (obj as StepViewModel).Step.TestId;
                int step = _getStep(ref maxUnboundIdx, StepSize, id, counts, testsLookup);

                stepsProxiesWithKeys[step] = obj;
            }

            return stepsProxiesWithKeys;
        }

        private int _getStep(ref int maxUnboundIdx, int stepSize, int parentId, Dictionary<int, int> counts, Dictionary<int, IndexedViewModel> lookup) {
            IndexedViewModel outVal;
            int step;

            if (lookup.TryGetValue(parentId, out outVal)) {
                int currCount;

                if (counts.TryGetValue(parentId, out currCount)) {
                    step = outVal.Index + (currCount + 1) * stepSize;
                    counts[parentId] += 1;
                } else {
                    step = outVal.Index + stepSize;
                    counts[parentId] = 1;
                }
            } else {
                maxUnboundIdx += stepSize;
                step = maxUnboundIdx;
            }

            return step;
        }

        #endregion

        #region classes

        private class MatchingResult {
            public Dictionary<int, ViewModelBase> Mapping { get; set; }
            public Dictionary<int, IndexedViewModel> Lookup { get; set; }
        }

        private class IndexedViewModel {
            public int Index { get; set; }
            public ViewModelBase ViewModel { get; set; }
        }

        #endregion

    }
}
