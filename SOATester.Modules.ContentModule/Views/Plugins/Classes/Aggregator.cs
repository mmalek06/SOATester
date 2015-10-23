using System;
using System.Collections.Generic;
using System.Collections.Specialized;

using SOATester.Modules.ContentModule.ViewModels;
using SOATester.Modules.ContentModule.ViewModels.Base;
using SOATester.Modules.ContentModule.Views.Plugins.Base;
using SOATester.Modules.ContentModule.Views.Plugins.Utils;
using SOATester.Modules.ContentModule.Views.Plugins.Utils.Enums;
using System.Linq;

namespace SOATester.Modules.ContentModule.Views.Plugins.Classes {
    public class TabAggregator : IPlugin {
        
        #region public properties

        public PluginKey PluginKey { get; set; }

        public Strategy Strategy { get; set; }

        public bool IsActive { get; set; }

        public int Priority { get; set; }

        #endregion

        #region constructors and destructors

        public TabAggregator() {
            PluginKey = PluginKey.AGGREGATOR;
            Strategy = Strategy.NONE;
        }

        #endregion

        #region public methods

        public IEnumerable<TabItemProxy> Execute(IEnumerable<TabItemProxy> objects) {
            if (objects != null) {
                var projectsMatchingResult = _matchProjectsWithKeys(objects);
                var scenariosMatchingResult = _matchScenariosWithKeys(objects, projectsMatchingResult.Lookup);
                var testsMatchingResult = _matchTestsWithKeys(objects, scenariosMatchingResult.Lookup);
                var stepsMatchingResult = _matchStepsWithKeys(objects, testsMatchingResult.Lookup);
                var dictionaries = new List<Dictionary<int, TabItemProxy>>();

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

        #region private methods

        private MatchingResult _matchProjectsWithKeys(IEnumerable<TabItemProxy> objects) {
            var projectsProxiesWithKeys = new Dictionary<int, TabItemProxy>();
            var projectsLookup = new Dictionary<int, IndexTabItemProxyPair>();
            const int StepSize = 100000;
            int step = StepSize;

            foreach (var obj in objects.Where(obj => obj.ViewModel is ProjectViewModel)) {
                projectsProxiesWithKeys[step] = obj;
                projectsLookup[(obj.ViewModel as ProjectViewModel).Id] = new IndexTabItemProxyPair {
                    Index = step,
                    ProxyObj = obj
                };

                step += StepSize;
            }

            return new MatchingResult {
                Mapping = projectsProxiesWithKeys,
                Lookup = projectsLookup
            };
        }

        private MatchingResult _matchScenariosWithKeys(IEnumerable<TabItemProxy> objects, Dictionary<int, IndexTabItemProxyPair> projectsLookup) {
            var scenariosProxiesWithKeys = new Dictionary<int, TabItemProxy>();
            var scenariosLookup = new Dictionary<int, IndexTabItemProxyPair>();
            var counts = new Dictionary<int, int>();
            int maxUnboundIdx = 100000;
            const int StepSize = 10000;
            int step = StepSize;

            foreach (var obj in objects.Where(obj => obj.ViewModel is ScenarioViewModel)) {
                var svm = obj.ViewModel as ScenarioViewModel;
                IndexTabItemProxyPair outVal;

                if (projectsLookup.TryGetValue(svm.Scenario.ProjectId, out outVal)) {
                    int currCount;

                    if (counts.TryGetValue(svm.Scenario.ProjectId, out currCount)) {
                        step = outVal.Index + (currCount + 1) * StepSize;
                        counts[svm.Scenario.ProjectId] += 1;
                    } else {
                        step = outVal.Index + StepSize;
                        counts[svm.Scenario.ProjectId] = 1;
                    }
                } else {
                    maxUnboundIdx += StepSize;
                    step = maxUnboundIdx;
                }

                scenariosProxiesWithKeys[step] = obj;
                scenariosLookup[(obj.ViewModel as ScenarioViewModel).Id] = new IndexTabItemProxyPair {
                    Index = step,
                    ProxyObj = obj
                };
            }

            return new MatchingResult {
                Mapping = scenariosProxiesWithKeys,
                Lookup = scenariosLookup
            };
        }

        private MatchingResult _matchTestsWithKeys(IEnumerable<TabItemProxy> objects, Dictionary<int, IndexTabItemProxyPair> scenariosLookup) {
            var testsProxiesWithKeys = new Dictionary<int, TabItemProxy>();
            var testsLookup = new Dictionary<int, IndexTabItemProxyPair>();
            var counts = new Dictionary<int, int>();
            int maxUnboundIdx = 100000;
            const int StepSize = 1000;
            int step = StepSize;

            foreach (var obj in objects.Where(obj => obj.ViewModel is TestViewModel)) {
                var tvm = obj.ViewModel as TestViewModel;
                IndexTabItemProxyPair outVal;

                if (scenariosLookup.TryGetValue(tvm.Test.ScenarioId, out outVal)) {
                    int currCount;

                    if (counts.TryGetValue(tvm.Test.ScenarioId, out currCount)) {
                        step = outVal.Index + (currCount + 1) * StepSize;
                        counts[tvm.Test.ScenarioId] += 1;
                    } else {
                        step = outVal.Index + StepSize;
                        counts[tvm.Test.ScenarioId] = 1;
                    }
                } else {
                    maxUnboundIdx += StepSize;
                    step = maxUnboundIdx;
                }

                testsProxiesWithKeys[step] = obj;
                testsLookup[(obj.ViewModel as TestViewModel).Id] = new IndexTabItemProxyPair {
                    Index = step,
                    ProxyObj = obj
                };
            }

            return new MatchingResult {
                Mapping = testsProxiesWithKeys,
                Lookup = testsLookup
            };
        }

        private Dictionary<int, TabItemProxy> _matchStepsWithKeys(IEnumerable<TabItemProxy> objects, Dictionary<int, IndexTabItemProxyPair> testSuitesLookup) {
            var stepsProxiesWithKeys = new Dictionary<int, TabItemProxy>();
            var counts = new Dictionary<int, int>();
            int maxUnboundIndex = 1000000;
            const int StepSize = 1000;
            int step = StepSize;

            foreach (var obj in objects.Where(obj => obj.ViewModel is StepViewModel)) {
                var svm = obj.ViewModel as StepViewModel;
                IndexTabItemProxyPair outVal;

                if (testSuitesLookup.TryGetValue(svm.Step.TestSuiteId, out outVal)) {
                    int currCount;

                    if (counts.TryGetValue(svm.Step.TestSuiteId, out currCount)) {
                        step = outVal.Index + (currCount + 1) * StepSize;
                        counts[svm.Step.TestSuiteId] += 1;
                    } else {
                        step = outVal.Index + StepSize;
                        counts[svm.Step.TestSuiteId] = 1;
                    }
                } else {
                    maxUnboundIndex += StepSize;
                    step = maxUnboundIndex;
                }

                stepsProxiesWithKeys[step] = obj;
            }

            return stepsProxiesWithKeys;
        }

        #endregion

        #region classes

        private class MatchingResult {
            public Dictionary<int, TabItemProxy> Mapping { get; set; }

            public Dictionary<int, IndexTabItemProxyPair> Lookup { get; set; }
        }

        private class IndexTabItemProxyPair {
            public int Index { get; set; }

            public TabItemProxy ProxyObj { get; set; }
        }

        #endregion

    }
}
