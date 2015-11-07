using SOATester.Communication.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SOATester.Communication {
    public interface IRunner<T> {
        Task<RunResult> RunAsync(T project);
        Task<IEnumerable<RunResult>> RunAsync(IEnumerable<T> project);

        Task StopAsync(T project);
        Task StopAsync(IEnumerable<T> project);

        Task PauseAsync(T project);
        Task PauseAsync(IEnumerable<T> project);
    }
}
