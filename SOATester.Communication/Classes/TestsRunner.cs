using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SOATester.Entities;

namespace SOATester.Communication {
    public class TestsRunner : ITestsRunner {

        #region public methods

        public void Pause(IEnumerable<Test> project) {
            throw new NotImplementedException();
        }

        public void Pause(Test project) {
            throw new NotImplementedException();
        }

        public void Run(IEnumerable<Test> project) {
            throw new NotImplementedException();
        }

        public void Run(Test project) {
            throw new NotImplementedException();
        }

        public void Stop(IEnumerable<Test> project) {
            throw new NotImplementedException();
        }

        public void Stop(Test project) {
            throw new NotImplementedException();
        }

        #endregion

    }
}
