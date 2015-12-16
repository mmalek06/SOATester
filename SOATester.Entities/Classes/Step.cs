using System.Collections.Generic;

namespace SOATester.Entities {
    public class Step {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Protocol { get; set; }
        public string Method { get; set; }
        public string RequestBody { get; set; }

        public virtual Test Test { get; set; }
        public virtual ICollection<RequestHeader> Headers { get; set; }

        public Step() {
            Headers = new List<RequestHeader>();
        }
    }
}