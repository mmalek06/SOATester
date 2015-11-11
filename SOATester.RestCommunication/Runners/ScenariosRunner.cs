using SOATester.Entities;
using SOATester.RestCommunication.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SOATester.RestCommunication {
    public class ScenariosRunner : IScenariosRunner {

        #region public methods

        public Task PauseAsync(IEnumerable<Scenario> project) {
            throw new NotImplementedException();
        }

        public Task PauseAsync(Scenario project) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RunResult>> RunAsync(IEnumerable<Scenario> project) {
            throw new NotImplementedException();
        }

        public Task<RunResult> RunAsync(Scenario project) {
            throw new NotImplementedException();
        }

        public Task StopAsync(IEnumerable<Scenario> project) {
            throw new NotImplementedException();
        }

        public Task StopAsync(Scenario project) {
            throw new NotImplementedException();
        }

        #endregion

    }
}
