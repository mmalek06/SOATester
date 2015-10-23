using System.Linq;
using System.Collections.Generic;

using Prism.Events;

using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SOATester.Modules.ContentModule.ViewModels;
using SOATester.Modules.ContentModule.Views.Plugins.Utils;
using SOATester.Modules.ContentModule.Views.Plugins.Base;
using SOATester.Modules.ContentModule.Views.Plugins.Utils.Enums;

using Tests.ContentModuletests.Base;

namespace Tests {
    [TestClass]
    public class PluginsTests : BaseTest {

        #region test methods

        [TestMethod]
        public void Test_AggregateStrategy_NONE() {
            var plugin = _getAggregateNonePlugin();
            var vmWrapper = new VmWrapper();
            var unorderedWrappedViewModels = vmWrapper.WrapObjects(_viewModels);
            var orderedWrappedViewModels = plugin.Execute(unorderedWrappedViewModels);
            var orderingReference = _getOrderingReference(unorderedWrappedViewModels);

            // false conditions
            Assert.IsFalse(Enumerable.SequenceEqual(orderingReference, unorderedWrappedViewModels), "Collections should not have the same ordering");
            Assert.IsFalse(Enumerable.SequenceEqual(unorderedWrappedViewModels, orderedWrappedViewModels), "Collections have the same ordering");

            // true conditions
            Assert.IsTrue(orderingReference.Count() == orderedWrappedViewModels.Count(), "Collections should have the same length");
            Assert.IsTrue(Enumerable.SequenceEqual(orderedWrappedViewModels, orderingReference), "Collections should have the same ordering");
        }

        #endregion

        #region helper methods

        private IPlugin _getAggregateNonePlugin() {
            var plugins = _container.Resolve<PluginFactory>().GetPlugins();

            return plugins.FirstOrDefault(plugin => plugin.PluginKey == PluginKey.AGGREGATOR && plugin.Strategy == Strategy.NONE);
        }

        private IEnumerable<TabItemProxy> _getOrderingReference(IEnumerable<TabItemProxy> unorderedWrappedViewModels) {
            var result = (new List<TabItemProxy> {
                // ProjectViewModel
                unorderedWrappedViewModels.FirstOrDefault(proxy => {
                    if (proxy.ViewModel is ProjectViewModel) {
                        return (proxy.ViewModel as ProjectViewModel).Id == 1;
                    }
                    return false;
                }),
                // TestSuiteViewModel
                unorderedWrappedViewModels.FirstOrDefault(proxy => {
                    if (proxy.ViewModel is TestViewModel) {
                        return (proxy.ViewModel as TestViewModel).Id == 1;
                    }
                    return false;
                }),
                // StepViewModel
                unorderedWrappedViewModels.FirstOrDefault(proxy => {
                    if (proxy.ViewModel is StepViewModel) {
                        return (proxy.ViewModel as StepViewModel).Id == 1;
                    }
                    return false;
                }),
                unorderedWrappedViewModels.FirstOrDefault(proxy => {
                    if (proxy.ViewModel is StepViewModel) {
                        return (proxy.ViewModel as StepViewModel).Id == 2;
                    }
                    return false;
                }),
                unorderedWrappedViewModels.FirstOrDefault(proxy => {
                    if (proxy.ViewModel is StepViewModel) {
                        return (proxy.ViewModel as StepViewModel).Id == 3;
                    }
                    return false;
                }),
                // TestSuiteViewModel
                unorderedWrappedViewModels.FirstOrDefault(proxy => {
                    if (proxy.ViewModel is TestViewModel) {
                        return (proxy.ViewModel as TestViewModel).Id == 2;
                    }
                    return false;
                }),
                // ProjectViewModel
                unorderedWrappedViewModels.FirstOrDefault(proxy => {
                    if (proxy.ViewModel is ProjectViewModel) {
                        return (proxy.ViewModel as ProjectViewModel).Id == 2;
                    }
                    return false;
                }),
                // TestSuiteViewModel
                unorderedWrappedViewModels.FirstOrDefault(proxy => {
                    if (proxy.ViewModel is TestViewModel) {
                        return (proxy.ViewModel as TestViewModel).Id == 3;
                    }
                    return false;
                })
            }).AsEnumerable();

            return result;
        }

        #endregion

    }
}
