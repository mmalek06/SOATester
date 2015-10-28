using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Events;

using SOATester.Entities;

using SOATester.Infrastructure.Events.Enums;
using SOATester.Infrastructure.Events.EventClasses;
using SOATester.Infrastructure.Events.Descriptors;

namespace SOATester.Communication {
    public class ProjectsRunner : IProjectsRunner {

        #region fields

        private IEventAggregator _eventAggregator;

        #endregion

        #region constructors and destructors

        public ProjectsRunner(IEventAggregator eventAggregator) {
            _eventAggregator = eventAggregator;
        }

        #endregion

        #region public methods

        public void Run(IEnumerable<Project> projects) {
            foreach (var project in projects) {
                Run(project);
            }
        }

        public void Run(Project project) {
            var evtDescriptor = new ItemRunEventDescriptor {
                Id = project.Id,
                ItemType = ChosenItemType.PROJECT,
                RunKind = RunKind.START
            };
            var runInfo = _getRunInfo(project);

            _eventAggregator.GetEvent<ItemRunEvent>().Publish(evtDescriptor);
        }

        public void Stop(IEnumerable<Project> projects) {
            foreach (var project in projects) {
                Stop(project);
            }
        }

        public void Stop(Project project) {
            var runInfo = _getRunInfo(project);
        }

        public void Pause(IEnumerable<Project> projects) {
            foreach (var project in projects) {
                Pause(project);
            }
        }

        public void Pause(Project project) {
            var runInfo = _getRunInfo(project);
        }

        #endregion

        #region methods

        private RunInfo _getRunInfo(Project project) {
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
