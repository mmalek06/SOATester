using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOATester.Entities {
    public class Scenario {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Test> Tests { get; set; }

        public Scenario() {
            Tests = new List<Test>();
        }
    }
}
