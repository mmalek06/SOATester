using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOATester.Modules.ContentModule.Repositories.Mock.Base {
    public abstract class MockRepository<T> {

        #region fields

        protected List<T> _cache;

        protected string _dataFileName;

        #endregion

        #region properties

        protected List<T> cache {
            get {
                if (_cache == null) {
                    _loadCache();
                }
                return _cache;
            }
        }

        #endregion

        #region protected methods

        protected void _loadCache() {
            using (StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"/MockData/" + _dataFileName)) {
                var itemsData = reader.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<T>>(itemsData);

                _cache = items;
            }
        }

        #endregion

    }
}
