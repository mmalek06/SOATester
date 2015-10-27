using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOATester.Modules.ContentModule.Models {
    public abstract class RunnableModel<T, K> {

        #region properties

        public T Model { get; set; }
        public IEnumerable<K> RunnableChildren { get; set; }

        #endregion

        #region public methods

        public abstract Uri GetAddress();

        #endregion

    }
}
