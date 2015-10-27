using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Events;
using Prism.Commands;

using SOATester.Infrastructure;
using SOATester.Infrastructure.Enums;

using SOATester.Entities;

using SOATester.Modules.ContentModule.Models;
using SOATester.Modules.ContentModule.ViewModels.Base;
using SOATester.Modules.ContentModule.Repositories.Base;

namespace SOATester.Modules.ContentModule.ViewModels {
    public class ScenarioViewModel : RunnableViewModel<Scenario, Test> {

        #region fields

        private Scenario _scenario;
        private string _name;
        private Uri _address;
        private Protocol _protocol;
        private ITestsRepository _testsRepository;
        
        #endregion

        #region properties

        public Scenario Scenario {
            get { return _scenario; }
            set {
                if (_scenario == null) {
                    SetProperty(ref _scenario, value);
                    _setupRunnableModel(value);
                }
            }
        }

        public int Id {
            get { return _scenario.Id; }
        }

        public string Name {
            get { return _name ?? _scenario.Name; }
            set { SetProperty(ref _name, value); }
        }

        public Uri Address {
            get { return _address ?? _scenario.Address; }
            set { SetProperty(ref _address, value); }
        }

        public Protocol Protocol {
            get { return _scenario.Protocol; }
            set { SetProperty(ref _protocol, value); }
        }

        #endregion

        #region constructors and destructors

        public ScenarioViewModel(IEventAggregator eventAggregator, ITestsRepository testsRepository) : base(eventAggregator) {
            _runnableModel = new RunnableScenario();
            _testsRepository = testsRepository;
        }

        #endregion

        #region event handlers

        protected override void _run() {
            throw new NotImplementedException();
        }

        protected override void _stop() {
            throw new NotImplementedException();
        }

        protected override void _pause() {
            throw new NotImplementedException();
        }

        #endregion

        #region methods

        private void _setupRunnableModel(Scenario scenario) {
            _runnableModel.Model = scenario;
            _runnableModel.RunnableChildren = _testsRepository.GetTestsForScenario(scenario);
        }

        #endregion

    }
}
