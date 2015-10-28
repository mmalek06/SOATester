using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOATester.Entities;

namespace SOATester.Communication {
    public class StepsRunner : IStepsRunner {

        #region public methods

        public void Pause(IEnumerable<Step> project) {
            throw new NotImplementedException();
        }

        public void Pause(Step project) {
            throw new NotImplementedException();
        }

        public void Run(IEnumerable<Step> project) {
            throw new NotImplementedException();
        }

        public void Run(Step project) {
            throw new NotImplementedException();
        }

        public void Stop(IEnumerable<Step> project) {
            throw new NotImplementedException();
        }

        public void Stop(Step project) {
            throw new NotImplementedException();
        }

        #endregion

    }
}
