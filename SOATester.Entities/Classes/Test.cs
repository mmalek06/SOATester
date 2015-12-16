using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOATester.Entities {
    public class Test : IParentEntity {
        public int Id { get; set; }
        public int ScenarioId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Protocol { get; set; }
        public string Method { get; set; }

        public virtual Scenario Scenario { get; set; }
        public virtual ICollection<Step> Steps { get; set; }
        public virtual ICollection<RequestHeader> Headers { get; set; }

        [NotMapped]
        public IEnumerable<object> Children { get { return Steps; } }

        public Test() { 
            Steps = new List<Step>();
            Headers = new List<RequestHeader>();
        }
    }
}
