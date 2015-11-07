using System.Linq;
using System.Collections.Generic;

using Prism.Events;

using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SOATester.Infrastructure;
using SOATester.Modules.ContentModule.ViewModels;
using SOATester.Modules.ContentModule.Plugins;
using SOATester.Modules.ContentModule.Plugins.Enums;
using SOATester.Modules.ContentModule.Plugins.Base;

using Tests.ContentModuletests.Base;

namespace Tests {
    [TestClass]
    public class PluginsTests : BaseTest {

        #region test methods

        [TestMethod]
        public void Test_AggregateStrategy_NONE() {
            var plugin = _getAggregateNonePlugin();
            var unorderedViewModels = _viewModels;
            var orderedViewModels = plugin.Execute(unorderedViewModels);
            var orderingReference = _getOrderingReference(unorderedViewModels);

            // false conditions
            Assert.IsFalse(Enumerable.SequenceEqual(orderingReference, unorderedViewModels), "Collections should not have the same ordering");
            Assert.IsFalse(Enumerable.SequenceEqual(unorderedViewModels, orderedViewModels), "Collections have the same ordering");

            // true conditions
            Assert.IsTrue(orderingReference.Count() == orderedViewModels.Count(), "Collections should have the same length");
            Assert.IsTrue(Enumerable.SequenceEqual(orderedViewModels, orderingReference), "Collections should have the same ordering");
        }

        #endregion

        #region helper methods

        private IPlugin _getAggregateNonePlugin() {
            var plugins = _container.Resolve<PluginFactory>().GetPlugins();

            return plugins.FirstOrDefault(plugin => plugin.PluginKey == PluginKey.AGGREGATOR && plugin.Strategy == Strategy.NONE);
        }

        private IEnumerable<ViewModelBase> _getOrderingReference(IEnumerable<ViewModelBase> unorderedWrappedViewModels) {
            var result = (new List<ViewModelBase> {
                // ProjectViewModel
                unorderedWrappedViewModels.FirstOrDefault(viewModel => {
                    if (viewModel is ProjectViewModel) {
                        return (viewModel as ProjectViewModel).Id == 1;
                    }
                    return false;
                }),
                // TestSuiteViewModel
                unorderedWrappedViewModels.FirstOrDefault(viewModel => {
                    if (viewModel is TestViewModel) {
                        return (viewModel as TestViewModel).Id == 1;
                    }
                    return false;
                }),
                // StepViewModel
                unorderedWrappedViewModels.FirstOrDefault(viewModel => {
                    if (viewModel is StepViewModel) {
                        return (viewModel as StepViewModel).Id == 1;
                    }
                    return false;
                }),
                unorderedWrappedViewModels.FirstOrDefault(viewModel => {
                    if (viewModel is StepViewModel) {
                        return (viewModel as StepViewModel).Id == 2;
                    }
                    return false;
                }),
                unorderedWrappedViewModels.FirstOrDefault(viewModel => {
                    if (viewModel is StepViewModel) {
                        return (viewModel as StepViewModel).Id == 3;
                    }
                    return false;
                }),
                // TestSuiteViewModel
                unorderedWrappedViewModels.FirstOrDefault(viewModel => {
                    if (viewModel is TestViewModel) {
                        return (viewModel as TestViewModel).Id == 2;
                    }
                    return false;
                }),
                // ProjectViewModel
                unorderedWrappedViewModels.FirstOrDefault(viewModel => {
                    if (viewModel is ProjectViewModel) {
                        return (viewModel as ProjectViewModel).Id == 2;
                    }
                    return false;
                }),
                // TestSuiteViewModel
                unorderedWrappedViewModels.FirstOrDefault(viewModel => {
                    if (viewModel is TestViewModel) {
                        return (viewModel as TestViewModel).Id == 3;
                    }
                    return false;
                })
            }).AsEnumerable();

            return result;
        }

        #endregion

    }
}
