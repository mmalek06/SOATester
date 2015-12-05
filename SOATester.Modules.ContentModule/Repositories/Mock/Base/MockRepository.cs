using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace SOATester.Modules.ContentModule.Repositories.Mock.Base {
    public abstract class MockRepository<T> {

        #region fields

        protected List<T> cache;
        protected string dataFileName;

        #endregion

        #region properties

        protected List<T> Cache {
            get {
                if (cache == null) {
                    LoadCache();
                }
                return cache;
            }
        }

        #endregion

        #region protected methods

        protected void LoadCache() {
            using (StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"/MockData/" + dataFileName)) {
                var itemsData = reader.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<T>>(itemsData);

                cache = items;
            }
        }

        #endregion

    }
}
