using SOATester.Infrastructure.Enums;
using System;
using System.Collections.Generic;

namespace SOATester.Entities.Base {
    public abstract class RequestModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Protocol { get; set; }
        public string Method { get; set; }

        public virtual ICollection<RequestHeader> Headers { get; set; }

        public RequestModel() {
            Headers = new List<RequestHeader>();
        }
    }
}
