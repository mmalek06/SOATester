using SOATester.Entities.Base;
using System.Collections.Generic;

namespace SOATester.Entities {
    public class Test : RequestModel {
        public int ScenarioId { get; set; }

        public virtual Scenario Scenario { get; set; }
        public virtual ICollection<Step> Steps { get; set; }

        public Test() : base() {
            Steps = new List<Step>();
        }
    }
}
