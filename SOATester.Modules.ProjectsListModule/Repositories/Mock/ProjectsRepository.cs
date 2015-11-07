using Newtonsoft.Json;
using SOATester.Entities;
using SOATester.Modules.ProjectsListModule.Repositories.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        #region methods

        private void _loadCache() {
            var notGroupedObjects = _getRawObjects();
            var cache = _getTransformedObjects(notGroupedObjects);

            _cache = cache;
        }

        private RawEntitiesContainer _getRawObjects() {
            var container = new RawEntitiesContainer();

            foreach (var pair in new[] {
                new Tuple<string, Type>("projects_data.json", typeof(Project)),
                new Tuple<string, Type>("scenarios_data.json", typeof(Scenario)),
                new Tuple<string, Type>("tests_data.json", typeof(Test)),
                new Tuple<string, Type>("steps_data.json", typeof(Step)) }) {
                using (StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"/MockData/" + pair.Item1)) {
                    var data = reader.ReadToEnd();
                    
                    if (pair.Item2 == typeof(Project)) {
                        container.Projects = JsonConvert.DeserializeObject<List<Project>>(data);
                    } else if (pair.Item2 == typeof(Scenario)) {
                        container.Scenarios = JsonConvert.DeserializeObject<List<Scenario>>(data);
                    } else if (pair.Item2 == typeof(Test)) {
                        container.Tests = JsonConvert.DeserializeObject<List<Test>>(data);
                    } else if (pair.Item2 == typeof(Step)) {
                        container.Steps = JsonConvert.DeserializeObject<List<Step>>(data);
                    }
                }
            }

            return container;
        }

        private List<Project> _getTransformedObjects(RawEntitiesContainer container) {
            var projects = new List<Project>();

            foreach (var project in container.Projects) {
                foreach (var scenario in from sc in container.Scenarios where sc.ProjectId == project.Id select sc) {
                    foreach (var test in from ts in container.Tests where ts.ScenarioId == scenario.Id select ts) {
                        foreach (var step in from st in container.Steps where st.TestId == test.Id select st) {
                            test.Steps.Add(step);
                        }

                        scenario.Tests.Add(test);
                    }

                    project.Scenarios.Add(scenario);
                }

                projects.Add(project);
            }

            return projects;
        }

        #endregion

        #region classes

        private class RawEntitiesContainer {
            public List<Project> Projects { get; set; }
            public List<Scenario> Scenarios { get; set; }
            public List<Test> Tests { get; set; }
            public List<Step> Steps { get; set; }
        }

        #endregion

    }
}
