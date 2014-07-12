﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModernUI.ExtendedToolkit
{
    /// <summary>
    /// Wizard Commands
    /// </summary>
    public static class WizardCommands
    {
        private static RoutedCommand _cancelCommand = new RoutedCommand();
        public static RoutedCommand Cancel
        {
            get
            {
                return _cancelCommand;
            }
        }

        private static RoutedCommand _finishCommand = new RoutedCommand();
        public static RoutedCommand Finish
        {
            get
            {
                return _finishCommand;
            }
        }

        private static RoutedCommand _helpCommand = new RoutedCommand();
        public static RoutedCommand Help
        {
            get
            {
                return _helpCommand;
            }
        }

        private static RoutedCommand _nextPageCommand = new RoutedCommand();
        public static RoutedCommand NextPage
        {
            get
            {
                return _nextPageCommand;
            }
        }

        private static RoutedCommand _previousPageCommand = new RoutedCommand();
        public static RoutedCommand PreviousPage
        {
            get
            {
                return _previousPageCommand;
            }
        }

        private static RoutedCommand _selectPageCommand = new RoutedCommand();
        public static RoutedCommand SelectPage
        {
            get
            {
                return _selectPageCommand;
            }
        }
    }
}
