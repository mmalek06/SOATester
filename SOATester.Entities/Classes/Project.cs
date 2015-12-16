using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOATester.Entities {
    public class Project : IParentEntity {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Protocol { get; set; }
        public string Method { get; set; }

        public virtual ICollection<RequestHeader> Headers { get; set; }
        public virtual ICollection<Scenario> Scenarios { get; set; }

        [NotMapped]
        public IEnumerable<object> Children { get { return Scenarios; } }

        public Project() { 
            Scenarios = new List<Scenario>();
            Headers = new List<RequestHeader>();
        }
    }
}
