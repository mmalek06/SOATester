using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOATester.Entities {
    public class Scenario : IParentEntity {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Protocol { get; set; }
        public string Method { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<RequestHeader> Headers { get; set; }
        public virtual ICollection<Test> Tests { get; set; }

        [NotMapped]
        public IEnumerable<object> Children { get { return Tests; } }

        public Scenario() { 
            Tests = new List<Test>();
            Headers = new List<RequestHeader>();
        }
    }
}
