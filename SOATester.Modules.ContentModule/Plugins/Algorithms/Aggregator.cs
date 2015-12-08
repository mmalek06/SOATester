using SOATester.Modules.ContentModule.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SOATester.Modules.ContentModule.Plugins {
    public class Aggregator : IPlugin {

        #region fields

        private const int MAX_10TH_POWER = 10;

        #endregion

        #region public properties

        public PluginKey PluginKey { get; set; }
        public Strategy Strategy { get; set; }
        public bool IsActive { get; set; }
        public int Priority { get; set; }

        #endregion

        #region constructors and destructors

        public Aggregator() {
            PluginKey = PluginKey.AGGREGATOR;
            Strategy = Strategy.NONE;
        }

        #endregion

        #region public methods

        public IEnumerable<IPluggableViewModel> Execute(IEnumerable<IPluggableViewModel> viewModels) {
            if (viewModels != null) {
                var groups = viewModels.GroupBy(vm => vm.GetType()).OrderBy(group => group.First().Importance);
                var dictionaries = new List<Dictionary<int, IPluggableViewModel>>();
                var lastLookup = new Dictionary<int, IndexedViewModel>();

                foreach (var group in groups) {
                    var matchingResult = Match(group, lastLookup);

                    lastLookup = matchingResult.Lookup;

                    dictionaries.Add(matchingResult.Mapping);
                }

                var flatDictionary = dictionaries.SelectMany(dict => dict).ToDictionary(pair => pair.Key, pair => pair.Value);
                var orderedEnumerable = flatDictionary.OrderBy(pair => pair.Key);
                var result = orderedEnumerable.Select(entry => entry.Value);

                return result;
            }

            return null;
        }

        #endregion

        #region methods

        private MatchingResult Match(IEnumerable<IPluggableViewModel> group, Dictionary<int, IndexedViewModel> lookup) {
            if (group.First().Importance == 1) {
                return MatchRoot(group);
            } else {
                return MatchChild(group, lookup);
            }
        }

        private MatchingResult MatchRoot(IEnumerable<IPluggableViewModel> group) {
            var proxiesWithKeys = new Dictionary<int, IPluggableViewModel>();
            var lookup = new Dictionary<int, IndexedViewModel>();
            int stepSize = (int)Math.Pow(10, MAX_10TH_POWER - group.First().Importance);
            int step = stepSize;

            foreach (var vm in group) { 
                proxiesWithKeys[step] = vm;
                lookup[vm.Id] = new IndexedViewModel {
                    Index = step,
                    ViewModel = vm
                };

                step += stepSize;
            }

            return new MatchingResult {
                Mapping = proxiesWithKeys,
                Lookup = lookup
            };
        }

        private MatchingResult MatchChild(IEnumerable<IPluggableViewModel> group, Dictionary<int, IndexedViewModel> lookup) {
            var proxiesWithKeys = new Dictionary<int, IPluggableViewModel>();
            var localLookup = new Dictionary<int, IndexedViewModel>();
            var counts = new Dictionary<int, int>();
            int maxUnboundIdx = (int)Math.Pow(10, MAX_10TH_POWER - group.First().Importance + 1);
            int stepSize = (int)Math.Pow(10, MAX_10TH_POWER - group.First().Importance);

            foreach (var vm in group) { 
                int id = vm.ParentId;
                int step = GetStep(ref maxUnboundIdx, stepSize, id, counts, lookup);

                proxiesWithKeys[step] = vm;
                localLookup[vm.Id] = new IndexedViewModel {
                    Index = step,
                    ViewModel = vm
                };
            }

            return new MatchingResult {
                Mapping = proxiesWithKeys,
                Lookup = localLookup
            };
        }

        private int GetStep(ref int maxUnboundIdx, int stepSize, int parentId, Dictionary<int, int> counts, Dictionary<int, IndexedViewModel> lookup) {
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

        private struct MatchingResult {
            public Dictionary<int, IPluggableViewModel> Mapping { get; set; }
            public Dictionary<int, IndexedViewModel> Lookup { get; set; }
        }

        private struct IndexedViewModel {
            public int Index { get; set; }
            public IPluggableViewModel ViewModel { get; set; }
        }

        #endregion

    }
}
