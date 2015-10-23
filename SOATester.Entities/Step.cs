using System.Collections.Generic;

namespace SOATester.Entities {
    public class Step {
        public int Id { get; set; }
        public int TestSuiteId { get; set; }
        public string Name { get; set; }
    }
}