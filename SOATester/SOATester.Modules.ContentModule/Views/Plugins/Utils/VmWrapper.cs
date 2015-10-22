using System.Collections.Generic;
using SOATester.Modules.ContentModule.ViewModels.Base;

namespace SOATester.Modules.ContentModule.Views.Plugins.Utils {
    public class VmWrapper {
        public TabItemProxy WrapObject(IViewModel parameter) {
            return new TabItemProxy {
                ViewModel = parameter
            };
        }

        public IEnumerable<TabItemProxy> WrapObjects(IEnumerable<IViewModel> parameter) {
            var result = new List<TabItemProxy>();

            foreach (var vm in parameter) {
                result.Add(WrapObject(vm));
            }

            return result;
        }
    }
}
