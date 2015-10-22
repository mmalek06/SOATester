using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SOATester.Entities;

using SOATester.Modules.ProjectsListModule.Repositories.Base;

namespace SOATester.Modules.ProjectsListModule.Repositories {
    public class ProjectsRepository : IProjectsRepository {

        #region fields

        private List<Project> _cache;

        #endregion

        #region public methods

        public IEnumerable<Project> GetProjects() {
            if (_cache == null) {
                _loadCache();
            }

            return _cache;
        }

        #endregion

        #region private methods
        
        private void _loadCache() {
            /*return new List<Project> {
                new Project {
                    Id = 1,
                    Name = "Rest",
                    TestSuites = new List<TestSuite> {
                        new TestSuite {
                            Id = 1,
                            Name = "Basic tests",
                            Steps = new List<Step> {
                                new Step { Id = 1, Name = "Step1" },
                                new Step { Id = 2, Name = "Step2" },
                                new Step { Id = 3, Name = "Step3" }
                            }
                        },
                        new TestSuite {
                            Id = 2,
                            Name = "Advanced tests",
                            Steps = new List<Step> {
                                new Step { Id = 4, Name = "Only one step" }
                            }
                        }
                    }
                },
                new Project {
                    Id = 2,
                    Name = "Soap",
                    TestSuites = new List<TestSuite> {
                        new TestSuite {
                            Id = 3,
                            Name = "Very basic tests",
                            Steps = new List<Step> {
                                new Step { Id = 5, Name = "OtherStep1" },
                                new Step { Id = 6, Name = "OtherStep2" },
                                new Step { Id = 7, Name = "OtherStep3" },
                                new Step { Id = 8, Name = "OtherStep4" }
                            }
                        }
                    }
                }
            };*/
        }

        #endregion

    }
}
