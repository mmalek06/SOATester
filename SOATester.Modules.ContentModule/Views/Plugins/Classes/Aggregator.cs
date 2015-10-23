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
                var testSuitesMatchingResult = _matchTestSuitesWithKeys(objects, projectsMatchingResult.Lookup);
                var stepsMatchingResult = _matchStepsWithKeys(objects, testSuitesMatchingResult.Lookup);
                var dictionaries = new List<Dictionary<int, TabItemProxy>>();

                dictionaries.Add(projectsMatchingResult.Mapping);
                dictionaries.Add(testSuitesMatchingResult.Mapping);
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
            int step = 10000;

            foreach (var obj in objects.Where(obj => obj.ViewModel is ProjectViewModel)) {
                projectsProxiesWithKeys[step] = obj;
                projectsLookup[(obj.ViewModel as ProjectViewModel).Id] = new IndexTabItemProxyPair {
                    Index = step,
                    ProxyObj = obj
                };

                step += 10000;
            }

            return new MatchingResult {
                Mapping = projectsProxiesWithKeys,
                Lookup = projectsLookup
            };
        }

        private MatchingResult _matchTestSuitesWithKeys(IEnumerable<TabItemProxy> objects, Dictionary<int, IndexTabItemProxyPair> projectsLookup) {
            var testSuitesProxiesWithKeys = new Dictionary<int, TabItemProxy>();
            var testSuitesLookup = new Dictionary<int, IndexTabItemProxyPair>();
            var counts = new Dictionary<int, int>();
            int maxUnboundIdx = 100000;
            int step = 1000;

            foreach (var obj in objects.Where(obj => obj.ViewModel is TestSuiteViewModel)) {
                var tsvm = obj.ViewModel as TestSuiteViewModel;
                IndexTabItemProxyPair outVal;

                if (projectsLookup.TryGetValue(tsvm.TestSuite.ProjectId, out outVal)) {
                    int currCount;

                    if (counts.TryGetValue(tsvm.TestSuite.ProjectId, out currCount)) {
                        step = outVal.Index + (currCount + 1) * 1000;
                        counts[tsvm.TestSuite.ProjectId] += 1;
                    } else {
                        step = outVal.Index + 1000;
                        counts[tsvm.TestSuite.ProjectId] = 1;
                    }
                } else {
                    maxUnboundIdx += 1000;
                    step = maxUnboundIdx;
                }

                testSuitesProxiesWithKeys[step] = obj;
                testSuitesLookup[(obj.ViewModel as TestSuiteViewModel).Id] = new IndexTabItemProxyPair {
                    Index = step,
                    ProxyObj = obj
                };
            }

            return new MatchingResult {
                Mapping = testSuitesProxiesWithKeys,
                Lookup = testSuitesLookup
            };
        }

        private Dictionary<int, TabItemProxy> _matchStepsWithKeys(IEnumerable<TabItemProxy> objects, Dictionary<int, IndexTabItemProxyPair> testSuitesLookup) {
            var stepsProxiesWithKeys = new Dictionary<int, TabItemProxy>();
            var counts = new Dictionary<int, int>();
            int maxUnboundIndex = 1000000;
            int step = 100;

            foreach (var obj in objects.Where(obj => obj.ViewModel is StepViewModel)) {
                var svm = obj.ViewModel as StepViewModel;
                IndexTabItemProxyPair outVal;

                if (testSuitesLookup.TryGetValue(svm.Step.TestSuiteId, out outVal)) {
                    int currCount;

                    if (counts.TryGetValue(svm.Step.TestSuiteId, out currCount)) {
                        step = outVal.Index + (currCount + 1) * 100;
                        counts[svm.Step.TestSuiteId] += 1;
                    } else {
                        step = outVal.Index + 100;
                        counts[svm.Step.TestSuiteId] = 1;
                    }
                } else {
                    maxUnboundIndex += 100;
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
