using Prism.Events;
using Prism.Mvvm;
using SOATester.Infrastructure.Events;
using System.Windows;

namespace SOATester {
    public class MainShellViewModel : BindableBase {

        #region fields

        private IEventAggregator eventAggregator;
        private string title;
        private SizeToContent sizeToContent;
        private WindowStyle windowStyle;
        private WindowState windowState;
        private Visibility loaderVisibility;
        private Visibility contentVisibility;

        #endregion

        #region properties

        public string Title {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public SizeToContent SizeToContent {
            get { return sizeToContent; }
            set { SetProperty(ref sizeToContent, value); }
        }

        public WindowStyle WindowStyle {
            get { return windowStyle; }
            set { SetProperty(ref windowStyle, value); }
        }

        public WindowState WindowState {
            get { return windowState; }
            set { SetProperty(ref windowState, value); }
        }

        public Visibility LoaderVisibility {
            get { return loaderVisibility; }
            set { SetProperty(ref loaderVisibility, value); }
        }

        public Visibility ContentVisibility {
            get { return contentVisibility; }
            set { SetProperty(ref contentVisibility, value); }
        }

        #endregion

        #region constructors and destructors

        public MainShellViewModel(IEventAggregator eventAggregator) {
            this.eventAggregator = eventAggregator;
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Normal;
            LoaderVisibility = Visibility.Visible;
            contentVisibility = Visibility.Collapsed;

            InitEvents();
        }

        #endregion

        #region methods

        private void InitEvents() {
            eventAggregator.GetEvent<BootingCompleted>().Subscribe(OnBootingCompleted);
        }

        #endregion

        #region event handlers

        private void OnBootingCompleted(bool completed) {
            Title = "SOA Tester";
            SizeToContent = SizeToContent.Manual;
            WindowStyle = WindowStyle.SingleBorderWindow;
            WindowState = WindowState.Maximized;
            LoaderVisibility = Visibility.Collapsed;
            ContentVisibility = Visibility.Visible;
        }

        #endregion

    }
}
