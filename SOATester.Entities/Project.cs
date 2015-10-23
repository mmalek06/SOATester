using System;
using System.Collections.Generic;

using SOATester.Infrastructure.Enums;

namespace SOATester.Entities {
    public class Project {
        public int Id { get; set; }
        public string Name { get; set; }
        public Uri Address { get; set; }
        public Protocol Protocol { get; set; }
        public Method Method { get; set; }

        public virtual ICollection<Parameter> Parameters { get; set; }
        public virtual ICollection<TestSuite> TestSuites { get; set; }

        public Project() {
            Parameters = new List<Parameter>();
            TestSuites = new List<TestSuite>();
        }
    }
}
