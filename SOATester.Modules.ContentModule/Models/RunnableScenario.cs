using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SOATester.Entities;

namespace SOATester.Modules.ContentModule.Models {
    public class RunnableScenario : RunnableModel<Scenario, Test> {

        #region methods

        public override Uri GetAddress() {
            return Model.Address;
        }

        #endregion

    }
}
