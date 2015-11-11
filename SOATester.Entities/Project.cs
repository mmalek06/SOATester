using SOATester.Entities.Base;
using System.Collections.Generic;

namespace SOATester.Entities {
    public class Project : RequestModel {
        public virtual ICollection<Scenario> Scenarios { get; set; }

        public Project() : base() {
            Scenarios = new List<Scenario>();
        }
    }
}
