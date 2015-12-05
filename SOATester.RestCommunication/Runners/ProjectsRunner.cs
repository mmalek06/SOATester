using SOATester.Entities;
using SOATester.RestCommunication.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOATester.RestCommunication {
    public class ProjectsRunner : IProjectsRunner {

        #region public methods

        public async Task<IEnumerable<RunResult>> RunAsync(IEnumerable<Project> projects) {
            var tasks = projects.Select(project => RunAsync(project)).ToArray();
            var result = await Task.WhenAll(tasks);

            return result;
        }

        public async Task<RunResult> RunAsync(Project project) {
            return await Task.Run(() => {
                return Run(project);
            });
        }

        public async Task StopAsync(IEnumerable<Project> projects) {
            
        }

        public async Task StopAsync(Project project) {
            
        }

        public async Task PauseAsync(IEnumerable<Project> projects) {
            
        }

        public async Task PauseAsync(Project project) {
            
        }

        #endregion

        #region methods

        private RunResult Run(Project project) {
            var runInfo = GetRunInfo(project);

            return new ProjectResult {
                Type = Enums.RunType.PROJECT,
                Status = Enums.RunStatus.SUCCESS
            };
        }

        private void Stop(Project project) {
            var runInfo = GetRunInfo(project);
        }

        private void Pause(Project project) {
            var runInfo = GetRunInfo(project);
        }

        private RunInfo GetRunInfo(Project project) {
            var scenarios = project.Scenarios;
            var tests = from scenario in scenarios
                        from test in scenario.Tests
                        select test;
            var steps = from test in tests
                        from step in test.Steps
                        select step;

            return new RunInfo {
                Scenarios = scenarios,
                Tests = tests,
                Steps = steps
            };
        }

        #endregion

        #region classes

        private class RunInfo {
            public IEnumerable<Scenario> Scenarios { get; set; }
            public IEnumerable<Test> Tests { get; set; }
            public IEnumerable<Step> Steps { get; set; }
        }

        #endregion

    }
}
