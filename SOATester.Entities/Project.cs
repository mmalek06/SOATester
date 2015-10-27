using System;
using System.Collections.Generic;

namespace SOATester.Entities {
    public class Project {
        public int Id { get; set; }
        public string Name { get; set; }
        public Uri Address { get; set; }
        
        public virtual ICollection<Parameter> Parameters { get; set; }
        public virtual ICollection<Scenario> Scenarios { get; set; }

        public Project() {
            Parameters = new List<Parameter>();
            Scenarios = new List<Scenario>();
        }
    }
}
