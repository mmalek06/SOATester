using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SOATester.TextEditor {
    public partial class EditorBase : UserControl {

        #region constructors and destructors

        public EditorBase() {
            InitializeComponent();

            SetEvents();
            SetDefaultAppearance();
        }

        #endregion

        #region setup logic

        private void SetEvents() {
            MouseEnter += ChangeCursorToCaret;
            MouseLeave += ChangeCursorToPointer;
        }

        private void SetDefaultAppearance() {
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("lightgray"));
            Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("white"));
            Height = 400;
            Width = 400;
        }

        #endregion

        #region event handlers

        private void ChangeCursorToPointer(object sender, MouseEventArgs e) => Mouse.OverrideCursor = Cursors.Arrow;

        private void ChangeCursorToCaret(object sender, MouseEventArgs e) {
            Mouse.OverrideCursor = Cursors.IBeam;
        }

        #endregion

    }
}
