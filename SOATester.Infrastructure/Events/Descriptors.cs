namespace SOATester.Infrastructure.Events {
    public class ItemChosenEventDescriptor {

        #region public properties

        public int Id { get; set; }
        public ChosenItemType ItemType { get; set; }

        #endregion

    }

    public class ItemRunEventDescriptor {

        #region public properties

        public int Id { get; set; }
        public RunKind RunKind { get; set; }
        public ChosenItemType ItemType { get; set; }

        #endregion

    }

    public class StartupEventDescriptor {

        #region public properties

        public string Message { get; set; }
        public StartupActivity Activity { get; set; }

        #endregion

    }
}
