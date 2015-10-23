using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOATester.Infrastructure.Plugins {
    public interface IPlugin {
        bool IsActive { get; set; }
        int Priority { get; set; }
    }
}
