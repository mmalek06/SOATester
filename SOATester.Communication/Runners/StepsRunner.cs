using SOATester.Communication.Base;
using SOATester.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SOATester.Communication {
    public class StepsRunner : IStepsRunner {

        #region public methods

        public Task PauseAsync(IEnumerable<Step> project) {
            throw new NotImplementedException();
        }

        public Task PauseAsync(Step project) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RunResult>> RunAsync(IEnumerable<Step> project) {
            throw new NotImplementedException();
        }

        public Task<RunResult> RunAsync(Step project) {
            throw new NotImplementedException();
        }

        public Task StopAsync(IEnumerable<Step> project) {
            throw new NotImplementedException();
        }

        public Task StopAsync(Step project) {
            throw new NotImplementedException();
        }

        #endregion

    }
}
