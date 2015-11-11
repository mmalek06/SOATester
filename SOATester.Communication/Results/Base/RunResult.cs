using SOATester.Communication.Enums;
using System.Collections.Generic;

namespace SOATester.Communication.Base {
    public abstract class RunResult {
        public RunStatus Status { get; set; }
        public RunType Type { get; set; }
        public IEnumerable<RunResult> PartialResults { get; set; }
    }
}
