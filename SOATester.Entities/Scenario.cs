using System;
using System.Collections.Generic;

using SOATester.Infrastructure.Enums;

namespace SOATester.Entities {
    public class Scenario {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public Uri Address { get; set; }
        public Protocol Protocol { get; set; }
        
        public virtual ICollection<Test> Tests { get; set; }

        public Scenario() {
            Tests = new List<Test>();
        }
    }
}
