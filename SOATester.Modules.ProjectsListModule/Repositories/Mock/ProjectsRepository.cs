using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using SOATester.Entities;

using SOATester.Modules.ProjectsListModule.Repositories.Base;

namespace SOATester.Modules.ProjectsListModule.Repositories.Mock {
    public class ProjectsRepository : IProjectsRepository {
        #region fields

        private IList<Project> _cache;

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
            var notGroupedObjects = _getRawObjects();
            var cache = _getTransformedObjects(notGroupedObjects);

            _cache = cache;
        }

        private RawEntitiesContainer _getRawObjects() {
            var container = new RawEntitiesContainer();

            using (StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"/MockData/projects_data.json")) {
                var projectsData = reader.ReadToEnd();
                var projects = JsonConvert.DeserializeObject<List<Project>>(projectsData);

                container.Projects = projects;
            }

            using (StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"/MockData/test_suites_data.json")) {
                var testSuitesData = reader.ReadToEnd();
                var testSuites = JsonConvert.DeserializeObject<List<TestSuite>>(testSuitesData);

                container.TestSuites = testSuites;
            }

            using (StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"/MockData/steps_data.json")) {
                var stepsData = reader.ReadToEnd();
                var steps = JsonConvert.DeserializeObject<List<Step>>(stepsData);

                container.Steps = steps;
            }

            return container;
        }

        private List<Project> _getTransformedObjects(RawEntitiesContainer container) {
            var projects = new List<Project>();

            foreach (var project in container.Projects) {
                foreach (var testSuite in from ts in container.TestSuites where ts.ProjectId == project.Id select ts) {
                    foreach (var step in from st in container.Steps where st.TestSuiteId == testSuite.Id select st) {
                        testSuite.Steps.Add(step);
                    }

                    project.TestSuites.Add(testSuite);
                }

                projects.Add(project);
            }

            return projects;
        }

        #endregion

        #region classes

        private class RawEntitiesContainer {
            public List<Project> Projects { get; set; }
            public List<TestSuite> TestSuites { get; set; }
            public List<Step> Steps { get; set; }
        }

        #endregion

    }
}
