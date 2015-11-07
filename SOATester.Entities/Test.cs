using SOATester.Infrastructure.Enums;
using System;
using System.Collections.Generic;

namespace SOATester.Entities {
    public class Test {
        public int Id { get; set; }
        public int ScenarioId { get; set; }
        public string Name { get; set; }
        public Uri Address { get; set; }
        public Protocol Protocol { get; set; }

        public virtual ICollection<Step> Steps { get; set; }

        public Test() {
            Steps = new List<Step>();
        }
    }
}
