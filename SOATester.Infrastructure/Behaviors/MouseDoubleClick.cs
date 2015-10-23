﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SOATester.Infrastructure.Behaviors {
    public class MouseDoubleClick {
        public static DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command",
            typeof(ICommand),
            typeof(MouseDoubleClick),
            new UIPropertyMetadata(_commandChanged));

        public static DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter",
                                                typeof(object),
                                                typeof(MouseDoubleClick),
                                                new UIPropertyMetadata(null));

        public static void SetCommand(DependencyObject target, ICommand value) {
            target.SetValue(CommandProperty, value);
        }

        public static void SetCommandParameter(DependencyObject target, object value) {
            target.SetValue(CommandParameterProperty, value);
        }
        public static object GetCommandParameter(DependencyObject target) {
            return target.GetValue(CommandParameterProperty);
        }

        protected static void _execute(object sender, RoutedEventArgs e) {
            Control control = sender as Control;
            var possibleTreeViewItem = sender as TreeViewItem;
            if (control == null || (possibleTreeViewItem != null && !possibleTreeViewItem.IsSelected)) {
                return;
            }
            ICommand command = (ICommand)control.GetValue(CommandProperty);
            object commandParameter = control.GetValue(CommandParameterProperty);
            command.Execute(commandParameter);
        }

        protected static void _commandChanged(DependencyObject target, DependencyPropertyChangedEventArgs e) {
            Control control = target as Control;
            if (control != null) {
                if ((e.NewValue != null) && (e.OldValue == null)) {
                    control.MouseDoubleClick += _execute;
                } else if ((e.NewValue == null) && (e.OldValue != null)) {
                    control.MouseDoubleClick -= _execute;
                }
            }
        }
    }
}