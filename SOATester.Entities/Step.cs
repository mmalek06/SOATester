using System;

using SOATester.Infrastructure.Enums;

namespace SOATester.Entities {
    public class Step {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string Name { get; set; }
        public Uri Address { get; set; }
        public Protocol Protocol { get; set; }
        public Method Method { get; set; }
    }
}