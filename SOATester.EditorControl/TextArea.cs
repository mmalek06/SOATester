using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace SOATester.EditorControl {
    /*public class TextArea : Control, IScrollInfo, IWeakEventListener {

        #region events

        /// <summary>
		/// Occurs when the TextArea receives text input.
		/// This is like the <see cref="UIElement.TextInput"/> event,
		/// but occurs immediately before the TextArea handles the TextInput event.
		/// </summary>
		public event TextCompositionEventHandler TextEntering;

        /// <summary>
        /// Occurs when the TextArea receives text input.
        /// This is like the <see cref="UIElement.TextInput"/> event,
        /// but occurs immediately after the TextArea handles the TextInput event.
        /// </summary>
        public event TextCompositionEventHandler TextEntered;

        #endregion

        #region IScrollInfo

        public bool CanHorizontallyScroll {
            get {
                throw new NotImplementedException();
            }

            set {
                throw new NotImplementedException();
            }
        }

        public bool CanVerticallyScroll {
            get {
                throw new NotImplementedException();
            }

            set {
                throw new NotImplementedException();
            }
        }

        public double ExtentHeight {
            get {
                throw new NotImplementedException();
            }
        }

        public double ExtentWidth {
            get {
                throw new NotImplementedException();
            }
        }

        public double HorizontalOffset {
            get {
                throw new NotImplementedException();
            }
        }

        public ScrollViewer ScrollOwner {
            get {
                throw new NotImplementedException();
            }

            set {
                throw new NotImplementedException();
            }
        }

        public double VerticalOffset {
            get {
                throw new NotImplementedException();
            }
        }

        public double ViewportHeight {
            get {
                throw new NotImplementedException();
            }
        }

        public double ViewportWidth {
            get {
                throw new NotImplementedException();
            }
        }

        public void LineDown() {
            throw new NotImplementedException();
        }

        public void LineLeft() {
            throw new NotImplementedException();
        }

        public void LineRight() {
            throw new NotImplementedException();
        }

        public void LineUp() {
            throw new NotImplementedException();
        }

        public Rect MakeVisible(Visual visual, Rect rectangle) {
            throw new NotImplementedException();
        }

        public void MouseWheelDown() {
            throw new NotImplementedException();
        }

        public void MouseWheelLeft() {
            throw new NotImplementedException();
        }

        public void MouseWheelRight() {
            throw new NotImplementedException();
        }

        public void MouseWheelUp() {
            throw new NotImplementedException();
        }

        public void PageDown() {
            throw new NotImplementedException();
        }

        public void PageLeft() {
            throw new NotImplementedException();
        }

        public void PageRight() {
            throw new NotImplementedException();
        }

        public void PageUp() {
            throw new NotImplementedException();
        }

        public void SetHorizontalOffset(double offset) {
            throw new NotImplementedException();
        }

        public void SetVerticalOffset(double offset) {
            throw new NotImplementedException();
        }

        #endregion

        #region IWeakEventListener

        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        #endregion

        #region Control

        protected override void OnTextInput(TextCompositionEventArgs e) {
            base.OnTextInput(e);

            if (!e.Handled && this.Document != null) {
                if (string.IsNullOrEmpty(e.Text) || e.Text == "\x1b" || e.Text == "\b") {
                    // Using some shortcuts would send an empty event
                    // ASCII 0x1b = ESC.
                    // A deadkey followed by backspace also raises this event
                    return;
                }

                PerformTextInput(e);
                e.Handled = true;
            }
        }

        protected virtual void OnTextEntering(TextCompositionEventArgs e) {
            if (TextEntering != null) {
                TextEntering(this, e);
            }
        }

        protected virtual void OnTextEntered(TextCompositionEventArgs e) {
            if (TextEntered != null) {
                TextEntered(this, e);
            }
        }

        #endregion

        #region TextArea public methods

        public void PerformTextInput(TextCompositionEventArgs e) {
            if (e == null)
                throw new ArgumentNullException("e");
            if (this.Document == null)
                throw ThrowUtil.NoDocumentAssigned();
            OnTextEntering(e);
            if (!e.Handled) {
                if (e.Text == "\n" || e.Text == "\r" || e.Text == "\r\n")
                    ReplaceSelectionWithNewLine();
                else {
                    if (OverstrikeMode && Selection.IsEmpty && Document.GetLineByNumber(Caret.Line).EndOffset > Caret.Offset)
                        EditingCommands.SelectRightByCharacter.Execute(null, this);
                    ReplaceSelectionWithText(e.Text);
                }
                OnTextEntered(e);
                caret.BringCaretToView();
            }
        }

        #endregion

    }*/
}
