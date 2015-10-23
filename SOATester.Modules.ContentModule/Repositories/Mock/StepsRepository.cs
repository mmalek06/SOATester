﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using SOATester.Entities;

using SOATester.Modules.ContentModule.Repositories.Base;
using SOATester.Modules.ContentModule.Repositories.Mock.Base;

namespace SOATester.Modules.ContentModule.Repositories.Mock {
    public class StepsRepository : MockRepository<Step>, IStepsRepository {

        #region constructors and destructors

        public StepsRepository() {
            _dataFileName = "steps_data.json";
        }

        #endregion
        
        #region public methods

        public Step GetStep(int id) {
            return cache.FirstOrDefault(item => item.Id == id);
        }

        public IEnumerable<Step> GetStepsForTestSuite(int testSuiteId) {
            return cache.Where(step => step.TestSuiteId == testSuiteId);
        }

        public IEnumerable<Step> GetStepsForTestSuite(TestSuite testSuite) {
            return GetStepsForTestSuite(testSuite.Id);
        }

        #endregion

    }
}