using SOATester.Entities;
using SOATester.RestCommunication.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SOATester.RestCommunication {
    public class TestsRunner : ITestsRunner {

        #region public methods

        public Task PauseAsync(IEnumerable<Test> project) {
            throw new NotImplementedException();
        }

        public Task PauseAsync(Test project) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RunResult>> RunAsync(IEnumerable<Test> project) {
            throw new NotImplementedException();
        }

        public Task<RunResult> RunAsync(Test project) {
            throw new NotImplementedException();
        }

        public Task StopAsync(IEnumerable<Test> project) {
            throw new NotImplementedException();
        }

        public Task StopAsync(Test project) {
            throw new NotImplementedException();
        }

        #endregion

    }
}
