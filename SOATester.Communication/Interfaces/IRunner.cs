using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SOATester.Entities;

namespace SOATester.Communication {
    public interface IRunner<T> {
        void Run(T project);
        void Run(IEnumerable<T> project);
        
        void Stop(T project);
        void Stop(IEnumerable<T> project);
        
        void Pause(T project);
        void Pause(IEnumerable<T> project);
    }
}
