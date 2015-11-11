using SOATester.Entities.Base;

namespace SOATester.Entities {
    public class Step : RequestModel {
        public int TestId { get; set; }
        public string RequestBody { get; set; }

        public virtual Test Test { get; set; }
    }
}