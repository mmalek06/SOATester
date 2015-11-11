using SOATester.Entities.Base;
using System.Collections.Generic;

namespace SOATester.Entities {
    public class Scenario : RequestModel {
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }        
        public virtual ICollection<Test> Tests { get; set; }

        public Scenario() : base() {
            Tests = new List<Test>();
        }
    }
}
