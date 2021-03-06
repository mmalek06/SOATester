﻿using SOATester.Modules.ContentModule.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SOATester.Modules.ContentModule.Plugins {
    public class Aggregator : PluginBase {

        #region fields

        private const int MAX_10TH_POWER = 10;

        #endregion

        #region constructors and destructors

        public Aggregator() : base() {
            PluginKey = PluginKey.AGGREGATOR;
            Strategy = Strategy.NONE;
        }

        #endregion

        #region public methods

        public override IEnumerable<PluggableViewModel> Execute(IEnumerable<PluggableViewModel> viewModels) {
            var groups = viewModels.GroupBy(vm => vm.GetType()).OrderBy(group => group.First().Importance);
            var dictionaries = new List<Dictionary<int, PluggableViewModel>>();
            var lastLookup = new Dictionary<int, IndexedViewModel>();

            foreach (var group in groups) {
                var matchingResult = Match(group, lastLookup);

                lastLookup = matchingResult.Lookup;

                dictionaries.Add(matchingResult.Mapping);
            }

            var flatDictionary = dictionaries.SelectMany(dict => dict).ToDictionary(pair => pair.Key, pair => pair.Value);
            var orderedEnumerable = flatDictionary.OrderBy(pair => pair.Key);
            var result = orderedEnumerable.Select(entry => entry.Value).ToArray();

            for (int i = 0; i < result.Length; i++) {
                result[i].PluggableProperties["Order"] = i;
            }

            return result;
        }

        #endregion

        #region methods

        private MatchingResult Match(IEnumerable<PluggableViewModel> group, Dictionary<int, IndexedViewModel> lookup) {
            if (group.First().Importance == 1) {
                return MatchRoot(group);
            } else {
                return MatchChild(group, lookup);
            }
        }

        private MatchingResult MatchRoot(IEnumerable<PluggableViewModel> group) {
            var proxiesWithKeys = new Dictionary<int, PluggableViewModel>();
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

        private MatchingResult MatchChild(IEnumerable<PluggableViewModel> group, Dictionary<int, IndexedViewModel> lookup) {
            var proxiesWithKeys = new Dictionary<int, PluggableViewModel>();
            var localLookup = new Dictionary<int, IndexedViewModel>();
            var counts = new Dictionary<int, int>();
            int maxUnboundIdx = (int)Math.Pow(10, MAX_10TH_POWER - group.First().Importance + 1);
            int stepSize = (int)Math.Pow(10, MAX_10TH_POWER - group.First().Importance);

            foreach (var vm in group) { 
                int step = GetStep(ref maxUnboundIdx, stepSize, vm.ParentId, vm.TopmostParentId, counts, lookup);

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

        private int GetStep(ref int maxUnboundIdx, int stepSize, int parentId, int topmostParentId, Dictionary<int, int> counts, Dictionary<int, IndexedViewModel> lookup) {
            IndexedViewModel outVal;
            int step;
            bool parentExists = lookup.TryGetValue(parentId, out outVal) || lookup.TryGetValue(topmostParentId, out outVal);

            if (parentExists) {
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
            public Dictionary<int, PluggableViewModel> Mapping { get; set; }
            public Dictionary<int, IndexedViewModel> Lookup { get; set; }
        }

        private struct IndexedViewModel {
            public int Index { get; set; }
            public PluggableViewModel ViewModel { get; set; }
        }

        #endregion

    }
}
