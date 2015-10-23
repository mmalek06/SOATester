using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOATester.Entities {
    public class TestSuite {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Step> Steps { get; set; }

        public TestSuite() {
            Steps = new List<Step>();
        }
    }
}
