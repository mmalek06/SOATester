namespace SOATester.Modules.ContentModule.Services {
    public interface IService<T> {

        #region public methods

        T Get(int id);
        bool Add(T obj);

        #endregion

    }
}
