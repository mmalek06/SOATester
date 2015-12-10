using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOATester.Modules.ContentModule.Repositories {
    public interface ISimpleRepository<T> {
        T GetEntity(int id);
    }
}
